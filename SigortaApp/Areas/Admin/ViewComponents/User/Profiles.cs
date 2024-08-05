using SigortaApp.BL.Abstract;
using SigortaApp.DAL.Abstract;
using SigortaApp.DAL.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace SigortaApp.Web.Areas.Admin.ViewComponents.User
{
	public class Profiles : ViewComponent
	{
		private readonly IUserService _userService;
		private readonly ITaskService _taskService;
		private readonly Context _context;
		private readonly IUnitOfWork _uow;

		public Profiles(IUserService userService, Context context, ITaskService taskService, IUnitOfWork uow)
		{
			_userService = userService;
			_context = context;
			_taskService = taskService;
			_uow = uow;
		}

		public IViewComponentResult Invoke()
		{

			ViewBag.ImageUrl = _userService.GetList().Where(w => w.Id == Convert.ToInt32(UserClaimsPrincipal.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value)).Select(s => s.ImageUrl).First();

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
