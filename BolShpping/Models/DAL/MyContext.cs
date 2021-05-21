using BolShpping.Models.BLL;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolShpping.Models.DAL
{
    public class MyContext : IdentityDbContext<AppUser>
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogImage> BlogImages { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set;  }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<Discountend> Discountends { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<File> Files { get; set; }
    }
}
