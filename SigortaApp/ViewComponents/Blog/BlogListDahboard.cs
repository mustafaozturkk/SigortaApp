using SigortaApp.BL.Abstract;
using SigortaApp.BL.Concrete;
using SigortaApp.DAL.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace SigortaApp.Web.ViewComponents.Blog
{
    public class BlogListDahboard : ViewComponent
    {
        private readonly IBlogService _blogService;

        public BlogListDahboard(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public IViewComponentResult Invoke()
        {
            var values = _blogService.GetListWithCategory();
            return View(values);
        }
    }
}
