using SigortaApp.BL.Abstract;
using SigortaApp.BL.Concrete;
using SigortaApp.BL.ValidationRules;
using SigortaApp.Entity.Concrete;
using SigortaApp.Web.Models;
using SigortaApp.DAL.Concrete;
using SigortaApp.DAL.EntityFramework;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SigortaApp.Web.Controllers
{
    public class WriterController : Controller
    {
        private readonly IWriterService _writerService;
        private readonly Context _context;

        private readonly UserManager<AppUser> _userManager;

        public WriterController(UserManager<AppUser> userManager, IWriterService writerService, Context context)
        {
            _userManager = userManager;
            _writerService = writerService;
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            var usermail = User.Identity.Name;
            ViewBag.v = usermail;
            Context c = _context;
            var writerName = c.Writers.Where(w => w.Mail == usermail).Select(y => y.Name).FirstOrDefault();
            ViewBag.v2 = writerName;
            return View();
        }
        public IActionResult WriterProfile()
        {
            return View();
        }
        public IActionResult WriterMail()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Test()
        {
            return View();
        }
        [AllowAnonymous]
        public PartialViewResult WriterNavbarPartial()
        {
            Context c = _context;
            var usermail = User.Identity.Name;
            var writerName = c.Writers.Where(w => w.Mail == usermail).Select(y => y.Name).FirstOrDefault();
            ViewBag.wn=writerName;
            return PartialView();
        }
        [AllowAnonymous]
        public PartialViewResult WriterFooterPartial()
        {
            return PartialView();
        }
        [HttpGet]
        public async Task<IActionResult> WriterEditProfile()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            UserUpdateViewModel model = new UserUpdateViewModel();
            model.Mail = values.Email;
            model.NameSurname = values.NameSurname;
            model.ImageUrl = values.ImageUrl;
            model.UserName = values.UserName;
            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> WriterEditProfile(UserUpdateViewModel model)
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            values.NameSurname = model.NameSurname;
            values.ImageUrl = model.ImageUrl;
            values.Email = model.Mail;
            values.PasswordHash = _userManager.PasswordHasher.HashPassword(values, model.Password);
            var result = await _userManager.UpdateAsync(values);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return View();
            }
            
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult WriterAdd()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult WriterAdd(AddProfileImage p)
        {
            Writer w = new Writer();
            if (p.Image != null)
            {
                var extension = Path.GetExtension(p.Image.FileName);
                var newimagename = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/WriterImageFiles/", newimagename);
                var stream = new FileStream(location, FileMode.Create);
                p.Image.CopyTo(stream);
                w.Image = newimagename;
            }
            w.Mail = p.Mail;
            w.Name = p.Name;
            w.Password = p.Password;
            w.Status = true;
            w.About = p.About;
            _writerService.TAdd(w);
            return RedirectToAction("Index","Dashboard");
        }

    }
}
