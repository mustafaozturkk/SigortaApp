using Microsoft.AspNetCore.Mvc;

namespace SigortaApp.Web.Controllers
{
    public class ErrorPageController : Controller
    {
        public IActionResult Error1(int code)
        {
            return View();
        }
    }
}
