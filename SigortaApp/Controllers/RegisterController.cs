using SigortaApp.BL.Abstract;
using SigortaApp.BL.Concrete;
using SigortaApp.BL.ValidationRules;
using SigortaApp.Entity.Concrete;
using SigortaApp.DAL.EntityFramework;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace SigortaApp.Web.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IWriterService _writerService;

        public RegisterController(IWriterService writerService)
        {
            _writerService = writerService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Writer writer)
        {
            WriterValidator wv = new WriterValidator();
            ValidationResult result = wv.Validate(writer);
            var m = Regex.Match(writer.Password, @"((?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,})");
            if (result.IsValid && m.Success)
            {
                writer.Status = true;
                writer.About = "Deneme Test";
                _writerService.TAdd(writer);
                return RedirectToAction("Index", "Blog");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                if (!m.Success)
                {
                    ViewBag.result = "Lütfen şifrenizi en az bir rakam, bir küçük harf ve bir büyük harf içermesine özen gösteriniz!";
                }
            }
            return View();
        }
    }
}
