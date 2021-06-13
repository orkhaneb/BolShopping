using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BolShpping.Models.BLL;
using BolShpping.Models.DAL;
using BolShpping.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace BolShpping
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = (context) => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(10);
                options.Cookie.IsEssential = true;
                options.Cookie.HttpOnly = false;
                options.Cookie.Name = "MyAppSession";
            });

         
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
              .AddCookie(options =>
              {
                  options.Cookie.HttpOnly = true;
                  options.Cookie.IsEssential = true;
                  options.Cookie.MaxAge = TimeSpan.FromMinutes(25);

              });


            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    );

            services.Configure<CookieOptions>(options =>
            {
                options.HttpOnly = true;
                options.IsEssential = true;
                options.MaxAge = TimeSpan.FromDays(1);
            });


            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                //Password
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;

                //Lock out
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 3;

                // User
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters =
               "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@";

            })
                .AddEntityFrameworkStores<MyContext>()
                  .AddDefaultUI()
                .AddDefaultTokenProviders();

            //For email sender
            services.AddSingleton<IEmailSender, EmailSender>(e => new EmailSender(
                _configuration["EmailSettings:Host"],
                _configuration.GetValue<int>("EmailSettings:Port"),
                _configuration.GetValue<bool>("EmailSettings:SSL"),
                _configuration["EmailSettings:Username"],
                _configuration["EmailSettings:Password"]

                ));
            services.AddDbContext<MyContext>(options =>
            {
                options.UseSqlServer(_configuration["ConnectionStrings:Default"]);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            //After the user has registered, the following must be connected in order for her name to appear when logged in.
            app.UseAuthentication();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                name: "areas", "WebCms",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{url?}");


            });
        }
    }
}
