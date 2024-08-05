using SigortaApp.BL.Abstract;
using SigortaApp.BL.Concrete;
using SigortaApp.Entity.Concrete;
using SigortaApp.DAL.Concrete;
using SigortaApp.DAL.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace SigortaApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MessageController : Controller
    {
        private readonly IMessage2Service _message2Service;
        private readonly Context _context;

        public MessageController(IMessage2Service message2Service, Context context)
        {
            _message2Service = message2Service;
            _context = context;
        }

        [Authorize(Roles ="Admin")]
        public IActionResult InBox()
        {
            var username = User.Identity.Name;
            var usermail = _context.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerId = _context.Writers.Where(w => w.Mail == usermail).Select(y => y.Id).FirstOrDefault();
            var values = _message2Service.GetInboxListByWriter(writerId);
            return View(values);
        }
        public IActionResult SendBox()
        {
            var username = User.Identity.Name;
            var usermail = _context.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerId = _context.Writers.Where(w => w.Mail == usermail).Select(y => y.Id).FirstOrDefault();
            var values = _message2Service.GetSendboxListByWriter(writerId);
            return View(values);
        }
        [HttpGet]
        public IActionResult ComposeMessage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ComposeMessage(Message2 p)
        {
            var username = User.Identity.Name;
            var usermail = _context.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerId = _context.Writers.Where(w => w.Mail == usermail).Select(y => y.Id).FirstOrDefault();
            p.SenderId = writerId;
            p.ReveiverId = 8;//ALICI AYARLANACAK
            p.Date = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            p.Status = true;
            _message2Service.TAdd(p);
            return RedirectToAction("SendBox");
        }
    }
}
