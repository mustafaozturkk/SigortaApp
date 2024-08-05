using SigortaApp.BL.Abstract;
using SigortaApp.BL.Concrete;
using SigortaApp.DAL.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace SigortaApp.Web.ViewComponents.Blog
{
    public class BlogLast3Post : ViewComponent
    {
        private readonly IBlogService _blogService;

        public BlogLast3Post(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public IViewComponentResult Invoke()
        {
            var values = _blogService.GetList().Take(3).ToList();//_blogService.GetLastThreeBlog();
            return View(values);
        }
    }
}
