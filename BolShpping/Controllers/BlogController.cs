using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BolShpping.Models.BLL;
using BolShpping.Models.DAL;
using BolShpping.Models.VM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BolShpping.Controllers
{
    public class BlogController : Controller
    {
        private readonly MyContext _context;
        private readonly UserManager<AppUser> _userManager;

        public BlogController(MyContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
         
            ViewModel vm = new ViewModel()
            {
                Blogs = await _context.Blogs.Include(p => p.BlogImages).ToListAsync(),
              
            };
            return View(vm);
        }

        public async Task<IActionResult> Details(int? id)
        {
            var singleBlog = await _context.Blogs.FindAsync(id);

            var modelid = _context.BlogImages?.Where(pi => pi.BlogId == singleBlog.Id).FirstOrDefault().ImageCode;

            var comments = await _context.Comments.Include(c => c.AppUser).OrderByDescending(c => c.DateTime).ToListAsync();
            var replies = await _context.Replies.Include(r => r.AppUser).OrderByDescending(c => c.DateTime).ToListAsync();
            ViewModel vm = new ViewModel()
            {
                Blog = singleBlog,
                Comments = comments,
                Replies = replies
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Comment(ViewModelComments comments)
        {

            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    status = 400
                });
            }
            else
            {
                try
                {
                    string appUserId = _userManager.GetUserId(HttpContext.User);
                    ViewBag.UserId = appUserId;
                    var user = await _userManager.FindByIdAsync(appUserId);
                    if (user != null)
                    {
                        Comment comment = new Comment();
                        comment.Message = comments.Text;
                        comment.AppUser = user;
                        comment.AppUserId = appUserId;
                        comment.Name = comments.Name;
                        comment.Website = comments.Website;
                        comment.Email = comments.Email;
                        comment.DateTime = DateTime.Now;

                        await _context.Comments.AddAsync(comment);
                        await _context.SaveChangesAsync();

                        return Json(new
                        {
                            status = 200,
                            data = comment
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            status = 400
                        });
                    }

                }
                catch (Exception ex)
                {
                    return Json(new
                    {
                        status = 500,
                        error = ex.Message
                    });
                }
            }


        }

        public async Task<IActionResult> ReplyComment(int id, ViewModelComments replyComment)
        {

            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    status = 400
                });
            }
            else
            {
                string appUserId = _userManager.GetUserId(HttpContext.User);
                var user = await _userManager.FindByIdAsync(appUserId);
                if (user != null)
                {
                    try
                    {
                        Reply reply = new Reply();
                        reply.Message = replyComment.Text;
                        reply.AppUser = user;
                        reply.AppUserId = appUserId;
                        reply.CommentId = id;
                        reply.Name = replyComment.Name;
                        reply.Website = replyComment.Website;
                        reply.Email = replyComment.Email;
                        reply.DateTime = DateTime.Now;
                        await _context.Replies.AddAsync(reply);
                        await _context.SaveChangesAsync();

                        return Json(new
                        {
                            status = 200,
                            data = reply
                        });
                    }
                    catch (Exception ex)
                    {
                        return Json(new
                        {
                            status = 500,
                            error = ex.Message
                        });
                    }
                }
                else
                {
                    return Json(new
                    {
                        status = 400
                    });
                }

            }
        }
    }
}
