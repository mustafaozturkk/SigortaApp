using SigortaApp.DAL.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace SigortaApp.Web.Areas.Admin.ViewComponents.Statistic
{
    public class Statistic2 : ViewComponent
    {
        private readonly Context _context;

        public Statistic2(Context context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.v1 = _context.Blogs.OrderByDescending(x => x.Id).Select(s => s.Title).Take(1).FirstOrDefault();
            return View();
        }
    }
}
