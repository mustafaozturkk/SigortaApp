using Microsoft.AspNetCore.Mvc;

namespace SigortaApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WidgetController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Index2()
        {
            return View();
        }
        public PartialViewResult NavbarPartial()
        {
            return PartialView();
        }
    }
}
