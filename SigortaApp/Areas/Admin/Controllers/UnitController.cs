using AspNetCoreHero.ToastNotification.Abstractions;
using SigortaApp.BL.Abstract;
using SigortaApp.DAL.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SigortaApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UnitController : Controller
    {
        private readonly Context _context;
        private readonly IUnitService _unitService;
        private readonly INotyfService _notyfService;
        private readonly IUserService _userService;

        public UnitController(Context context, IUnitService unitService, INotyfService notyfService, IUserService userService)
        {
            _context = context;
            _unitService = unitService;
            _notyfService = notyfService;
            _userService = userService;
        }

        [Authorize(Roles ="Admin")]
        public IActionResult Index()
        {
            var result = _unitService.GetList();
            return View(result);
        }
    }
}
