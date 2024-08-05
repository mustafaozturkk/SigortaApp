using SigortaApp.BL.Abstract;
using SigortaApp.BL.Concrete;
using SigortaApp.Entity.Concrete;
using SigortaApp.DAL.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace SigortaApp.Web.Controllers
{
    [AllowAnonymous]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public PartialViewResult PartialAddComment()
        {
            return PartialView();
        }
        [HttpPost]
        public PartialViewResult PartialAddComment(Comment c)
        {
            c.Date = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.Status = true;
            c.BlogId = 2;
            _commentService.AddComment(c);
            return PartialView();
        }
        public PartialViewResult CommentListByBlog(int id)
        {
            var values = _commentService.GetList(id);
            return PartialView(values);
        }
    }
}
