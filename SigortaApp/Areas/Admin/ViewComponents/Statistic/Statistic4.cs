using SigortaApp.DAL.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace SigortaApp.Web.Areas.Admin.ViewComponents.Statistic
{
    public class Statistic4 : ViewComponent
    {
        private readonly Context _context;

        public Statistic4(Context context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.v1 = _context.Admins.Where(x => x.Id == 1).Select(y => y.Name).FirstOrDefault();
            ViewBag.v2 = _context.Admins.Where(x => x.Id == 1).Select(y => y.ImageURL).FirstOrDefault();
            ViewBag.v3 = _context.Admins.Where(x => x.Id == 1).Select(y => y.ShortDescription).FirstOrDefault();
            return View();
        }
    }
}
