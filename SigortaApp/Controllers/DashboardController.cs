using SigortaApp.DAL.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace SigortaApp.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly Context _context;

        public DashboardController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            var username = User.Identity.Name;
            ViewBag.veri = username;
            var usermail = _context.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerId = _context.Writers.Where(w => w.Mail == usermail).Select(s => s.Id).FirstOrDefault();
            var writerName = _context.Writers.Where(w => w.Mail == usermail).Select(s => s.Name).FirstOrDefault();

            ViewBag.v1 = _context.Blogs.Count().ToString();
            ViewBag.v2 = _context.Blogs.Where(x => x.WriterId == writerId).Count().ToString();
            ViewBag.v3 = _context.Categories.Count().ToString();
            ViewBag.vn = writerName;
            return View();
        }
    }
}
