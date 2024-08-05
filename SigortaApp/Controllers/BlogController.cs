using SigortaApp.BL.Abstract;
using SigortaApp.BL.Concrete;
using SigortaApp.BL.ValidationRules;
using SigortaApp.Entity.Concrete;
using SigortaApp.DAL.Concrete;
using SigortaApp.DAL.EntityFramework;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SigortaApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ICategoryService _categoryService;
        private readonly Context _context;

        public BlogController(IBlogService blogService, ICategoryService categoryService, Context context)
        {
            _blogService = blogService;
            _categoryService = categoryService;
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var values = _blogService.GetListWithCategory();
            return View(values);
        }
        [AllowAnonymous]
        public IActionResult ReadAll(int id)
        {
            ViewBag.i = id;
            var values = _blogService.TGetById(id);
            return View(values);
        }
        public IActionResult BlogListByWriter()
        {
            var username = User.Identity.Name;
            var usermail = _context.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerId = _context.Writers.Where(w => w.Mail == usermail).Select(y => y.Id).FirstOrDefault();
            var values = _blogService.GetListByWriter(writerId);//_blogService.GetListWithCategoryByWriterBm(writerId);
            return View(values);
        }

        public void CategoryList()
        {
            List<SelectListItem> categoryValues = (from x in _categoryService.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.Name,
                                                       Value = x.Id.ToString()
                                                   }).ToList();
            ViewBag.cv = categoryValues;
        }

        [HttpGet]
        public IActionResult BlogAdd()
        {
            CategoryList();
            return View();
        }

        [HttpPost]
        public IActionResult BlogAdd(Blog blog)
        {
            CategoryList();
            BlogValidator bv = new BlogValidator();
            ValidationResult result = bv.Validate(blog);

            var username = User.Identity.Name;
            var usermail = _context.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerId = _context.Writers.Where(w => w.Mail == usermail).Select(y => y.Id).FirstOrDefault();

            if (result.IsValid)
            {
                blog.Status = true;
                blog.CreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                blog.WriterId = writerId;
                _blogService.TAdd(blog);
                return RedirectToAction("BlogListByWriter", "Blog");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

        public IActionResult DeleteBlog(int Id)
        {
            var blogValue = _blogService.TGetById(Id);
            _blogService.TDelete(blogValue);
            return RedirectToAction("BlogListByWriter");
        }

        [HttpGet]
        public IActionResult EditBlog(int id)
        {
            var blogValue = _blogService.TGetById(id);
            CategoryList();
            return View(blogValue);
        }

        [HttpPost]
        public IActionResult EditBlog(Blog p)
        {
            var username = User.Identity.Name;
            var usermail = _context.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerId = _context.Writers.Where(w => w.Mail == usermail).Select(y => y.Id).FirstOrDefault();
            p.WriterId = writerId;
            p.Status = true;
            p.CreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            _blogService.TUpdate(p);
            return RedirectToAction("BlogListByWriter");
        }
    }
}
