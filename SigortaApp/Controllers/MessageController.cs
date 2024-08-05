using SigortaApp.BL.Abstract;
using SigortaApp.BL.Concrete;
using SigortaApp.Entity.Concrete;
using SigortaApp.DAL.Concrete;
using SigortaApp.DAL.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace SigortaApp.Web.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessage2Service _message2Service;
        private readonly Context _context;

        public MessageController(IMessage2Service message2Service, Context context)
        {
            _message2Service = message2Service;
            _context = context;
        }

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

        public IActionResult MessageDetails(int id)
        {
            var value = _message2Service.TGetById(id);
            return View(value);
        }

        [HttpGet]
        public IActionResult SendMessage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(Message2 message2)
        {
            var username = User.Identity.Name;
            var usermail = _context.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerId = _context.Writers.Where(w => w.Mail == usermail).Select(y => y.Id).FirstOrDefault();
            message2.SenderId = writerId;
            message2.ReveiverId = 5;
            message2.Status = true;
            message2.Date = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            _message2Service.TAdd(message2);
            return RedirectToAction("InBox");
        }
    }
}
