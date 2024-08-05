using SigortaApp.BL;
using SigortaApp.BL.Abstract;
using SigortaApp.Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Enums = SigortaApp.Entity.Enums;

namespace SigortaApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly ITaskHistoryService _taskHistoryService;
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICalendarService _calendarService;

		public HomeController(ITaskService taskService, ITaskHistoryService taskHistoryService, IUserService userService, UserManager<AppUser> userManager, ICalendarService calendarService)
		{
            _taskService = taskService;
            _taskHistoryService = taskHistoryService;
            _userService = userService;
            _userManager = userManager;
            _calendarService = calendarService;
		}

		public IActionResult Index()
        {
            var task = _taskService.GetList();

            var totalTaskCount = task;
            var totalDayTaskCount = task.Where(w => w.CreatedDate.Date == DateTime.Now.Date);

            var totalEndTaskCount = task.Where(w => w.StatusId == (int)Enums.StatusEnum.IsBitirildi).ToList();
            var totalDayEndTaskCount = totalEndTaskCount.Where(w => w.CreatedDate.Date == DateTime.Now.Date);

            var otogardaBekleyenCount = task.Where(w => w.StatusId == (int)Enums.StatusEnum.OtogaraYonlendirildi);

            if (User.Claims.Where(w => w.Type.Contains("role")).First().Value == Enums.RoleEnum.Kurye.ToString())
            {
                totalTaskCount = task.Where(w => w.Carrier == Convert.ToInt32(User.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value)).ToList();
                totalDayTaskCount = task.Where(w => w.CreatedDate.Date == DateTime.Now.Date && w.Carrier == Convert.ToInt32(User.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value)).ToList();

                totalEndTaskCount = task.Where(w => w.StatusId == (int)Enums.StatusEnum.IsBitirildi && w.Carrier == Convert.ToInt32(User.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value)).ToList();
                totalDayEndTaskCount = totalEndTaskCount.Where(w => w.CreatedDate.Date == DateTime.Now.Date && w.Carrier == Convert.ToInt32(User.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value)).ToList();

                otogardaBekleyenCount = task.Where(w => w.StatusId == (int)Enums.StatusEnum.OtogaraYonlendirildi && w.Carrier == Convert.ToInt32(User.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value)).ToList();
            }
            else if(User.Claims.Where(w => w.Type.Contains("role")).First().Value == Enums.RoleEnum.Otogar.ToString())
            {
                totalTaskCount = task.ToList();
                totalDayTaskCount = task.Where(w => w.CreatedDate.Date == DateTime.Now.Date).ToList();

                totalEndTaskCount = task.Where(w => w.StatusId == (int)Enums.StatusEnum.IsBitirildi).ToList();
                totalDayEndTaskCount = totalEndTaskCount.Where(w => w.CreatedDate.Date == DateTime.Now.Date).ToList();

                otogardaBekleyenCount = task.Where(w => w.StatusId == (int)Enums.StatusEnum.OtogaraYonlendirildi).ToList();
            }
            else if(User.Claims.Where(w => w.Type.Contains("role")).First().Value == Enums.RoleEnum.Kullanici.ToString())
            {
                task = task.Where(w => w.CreatedBy == Convert.ToInt32(User.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value)).ToList();
                totalTaskCount = task.ToList();
                totalDayTaskCount = task.Where(w => w.CreatedDate.Date == DateTime.Now.Date).ToList();

                totalEndTaskCount = task.Where(w => w.StatusId == (int)Enums.StatusEnum.IsBitirildi).ToList();
                totalDayEndTaskCount = totalEndTaskCount.Where(w => w.CreatedDate.Date == DateTime.Now.Date).ToList();

                otogardaBekleyenCount = task.Where(w => w.StatusId == (int)Enums.StatusEnum.OtogaraYonlendirildi).ToList();
            }
            
            ViewBag.TotalTaskCount = totalTaskCount.Count();
            ViewBag.TotalDayTaskCount = totalDayTaskCount.Count();

            ViewBag.TotalEndTaskCount = totalEndTaskCount.Count();
            ViewBag.TotalDayEndTaskCount = totalDayEndTaskCount.Count();

            ViewBag.OtogardaBekleyenCount = otogardaBekleyenCount.Count();

            return View();
        }

        public IActionResult TestCalendar()
        {
            return View();
        }

        [HttpPost("/Home/GetCalendar")]
        public IActionResult GetCalendar()
        {
            var list = _calendarService.GetList();
            return Json(list);
        }

        [HttpPost("/Home/GetCalendarById/id")]
        public IActionResult GetCalendarById(int id)
        {
            var result = _calendarService.TGetById(id);
            return Json(result);
        }

        [HttpPost("/Home/AddCalendar")]
        public IActionResult AddCalendar(Calendar calendar)
        {
            if (calendar.Id == 0)
            {
                calendar.CreatedBy = Convert.ToInt32(User.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value);
                calendar.CreatedDate = DateTime.Now;
                _calendarService.TAdd(calendar);
            }
            else
            {
                _calendarService.TUpdate(calendar);
            }
            return RedirectToAction("TestCalendar");
        }

        [HttpPost("/Home/EditSurukleCalendar")]
        public IActionResult EditSurukleCalendar(Calendar calendar)
        {
            var result = _calendarService.TGetById(calendar.Id);
            result.start = calendar.start;
            result.end = calendar.end;
            _calendarService.TUpdate(result);
            return RedirectToAction("TestCalendar");
        }
    }
}
