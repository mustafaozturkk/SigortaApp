using SigortaApp.BL.Abstract;
using SigortaApp.BL.Concrete;
using SigortaApp.DAL.Concrete;
using SigortaApp.DAL.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace SigortaApp.Web.ViewComponents.Writer
{
    public class WriterMessageNotification : ViewComponent
    {
        private readonly IMessage2Service _message2Service;
        private readonly Context _context;

        public WriterMessageNotification(IMessage2Service message2Service, Context context)
        {
            _message2Service = message2Service;
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var username = User.Identity.Name;
            var usermail = _context.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerId = _context.Writers.Where(w => w.Mail == usermail).Select(y => y.Id).FirstOrDefault();
            var values = _message2Service.GetInboxListByWriter(writerId);
            return View(values);
        }
    }
}