using SigortaApp.BL.Abstract;
using SigortaApp.BL.Concrete;
using SigortaApp.Entity.Concrete;
using SigortaApp.DAL.Concrete;
using SigortaApp.DAL.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace SigortaApp.Web.ViewComponents.Writer
{
    public class WriterAboutOnDashboard : ViewComponent
    {
        private readonly IWriterService _writerService;

        private readonly Context _context;

        public WriterAboutOnDashboard(IWriterService writerService, Context context)
        {
            _writerService = writerService;
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var username = User.Identity.Name;
            ViewBag.veri = username;
            var usermail = _context.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerId = _context.Writers.Where(w => w.Mail == usermail).Select(y => y.Id).FirstOrDefault();
            var values = _writerService.GetWriterById(writerId);
            return View(values);
        }
    }
}
