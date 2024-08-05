using SigortaApp.BL.Abstract;
using SigortaApp.Entity.Concrete;
using SigortaApp.DAL.Abstract;
using SigortaApp.DAL.Concrete;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SigortaApp.Web.Areas.Admin.ViewComponents.Notifications
{
    public class Notifications : ViewComponent
    {
        private readonly IUserService _userService;
        private readonly ITaskService _taskService;
        private readonly Context _context;
        private readonly IUnitOfWork _uow;

        public Notifications(IUserService userService, ITaskService taskService, Context context, IUnitOfWork uow)
        {
            _userService = userService;
            _taskService = taskService;
            _context = context;
            _uow = uow;
        }

        public IViewComponentResult Invoke()
        {
            List<Task> result = new List<Task>();

            if (UserClaimsPrincipal.Claims.Where(w => w.Type.Contains("role")).First().Value == "Admin")
            {
                result = _taskService.GetList().OrderByDescending(o => o.CreatedDate).Take(3).ToList();
                ViewBag.TaskCount = result.Count();
            }
            else
            {
                result = _taskService.GetList().Where(w => w.Carrier == Convert.ToInt32(UserClaimsPrincipal.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value)).ToList().OrderByDescending(o => o.CreatedDate).Take(3).ToList();
                ViewBag.TaskCount = result.Count();
            }

            //ViewBag.MesajSayisi = _context.Contacts.Count();
            //ViewBag.YorumSayisi = _context.Comments.Count();

            //string api = "997b5b55b05f3eb92952b60796b9bec9";
            //string connection = "https://api.openweathermap.org/data/2.5/weather?q=Ankara&mode=xml&lang=tr&units=metric&appid=" + api;
            //XDocument document = XDocument.Load(connection);
            //ViewBag.v4 = document.Descendants("temperature").ElementAt(0).Attribute("value").Value;
            return View(result);
        }
    }
}
