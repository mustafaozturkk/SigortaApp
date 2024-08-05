using SigortaApp.BL.Abstract;
using SigortaApp.BL.Concrete;
using SigortaApp.DAL.Abstract;
using SigortaApp.DAL.Concrete;
using SigortaApp.DAL.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Xml.Linq;

namespace SigortaApp.Web.Areas.Admin.ViewComponents.User
{
    public class UserDatas : ViewComponent
    {
        private readonly IUserService _userService;
        private readonly ITaskService _taskService;
        private readonly Context _context;
        private readonly IUnitOfWork _uow;

        public UserDatas(IUserService userService, Context context, ITaskService taskService,IUnitOfWork uow)
        {
            _userService = userService;
            _context = context;
            _taskService = taskService;
            _uow = uow;
        }

        public IViewComponentResult Invoke()
        {
            var taskList = _taskService.GetListWithCompanyCityAndStatus();

            if (UserClaimsPrincipal.Claims.Where(w => w.Type.Contains("role")).First().Value == Entity.Enums.RoleEnum.Admin.ToString())
            {
                ViewBag.TaskCount = taskList.Count();
            }
            else if (UserClaimsPrincipal.Claims.Where(w => w.Type.Contains("role")).First().Value == Entity.Enums.RoleEnum.Otogar.ToString())
            {
                ViewBag.TaskCount = taskList.Where(w => w.Carrier == Convert.ToInt32(UserClaimsPrincipal.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value)).Count();
            }
            else if (UserClaimsPrincipal.Claims.Where(w => w.Type.Contains("role")).First().Value == Entity.Enums.RoleEnum.Kullanici.ToString())
            {
                ViewBag.TaskCount = taskList.Where(w => w.CreatedBy == Convert.ToInt32(UserClaimsPrincipal.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value)).Count();
            }
            else
            {
                ViewBag.TaskCount = taskList.Where(w => w.Carrier == Convert.ToInt32(UserClaimsPrincipal.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value) || (w.StatusId == (int)Entity.Enums.StatusEnum.IsOlusturuldu || w.StatusId == (int)Entity.Enums.StatusEnum.BeklemeyeAlindi && w.Carrier == null)).Count();
            }

            //ViewBag.MesajSayisi = _context.Contacts.Count();
            //ViewBag.YorumSayisi = _context.Comments.Count();

            //string api = "997b5b55b05f3eb92952b60796b9bec9";
            //string connection = "https://api.openweathermap.org/data/2.5/weather?q=Ankara&mode=xml&lang=tr&units=metric&appid=" + api;
            //XDocument document = XDocument.Load(connection);
            //ViewBag.v4 = document.Descendants("temperature").ElementAt(0).Attribute("value").Value;
            return View();
        }
    }
}
