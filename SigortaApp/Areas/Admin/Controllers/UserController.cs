using AspNetCoreHero.ToastNotification.Abstractions;
using SigortaApp.Entity.Concrete;
using SigortaApp.Web.Models;
using SigortaApp.DAL.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using System.Collections;
using Microsoft.AspNetCore.Authorization;

namespace SigortaApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
		private readonly Context _context;
		private readonly INotyfService _notyfService;
		private readonly UserManager<AppUser> _userManager;

		public UserController(Context context, INotyfService notyfService, UserManager<AppUser> userManager)
		{
			_context = context;
			_notyfService = notyfService;
			_userManager = userManager;
		}

		[Authorize(Roles ="Admin")]
		public IActionResult Index()
        {
			return View();
        }

		[HttpGet]
		public async Task<IActionResult> UserEditProfile()
		{
			var values = await _userManager.FindByNameAsync(User.Identity.Name);
			var values2 = await _userManager.GetRolesAsync(values);
			UserUpdateViewModel model = new UserUpdateViewModel();
			model.Mail = values.Email;
			model.NameSurname = values.NameSurname;
			model.UserName = values.UserName;
			model.Role = values2[0].ToString();
			model.ImageUrl = values.ImageUrl;
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> UserEditProfile(UserUpdateViewModel model)
		{
			var values = await _userManager.FindByNameAsync(User.Identity.Name);
			values.Email = model.Mail;
			values.PasswordHash = _userManager.PasswordHasher.HashPassword(values, model.Password);
			var result = await _userManager.UpdateAsync(values);
            if (result.Succeeded)
			{
                await _userManager.UpdateSecurityStampAsync(values);
                _notyfService.Success("Başarılı!");
				return RedirectToAction("UserEditProfile");
			}
			else
			{
				return View();
			}
		}

		[HttpPost]
		public async Task<IActionResult> UploadImage(IFormFile file)
		{
			if (file != null)
			{
				//Eski sistem

				var memoryStream = new MemoryStream();
				await file.CopyToAsync(memoryStream);

				if (memoryStream.Length < 2097152)
				{
					string imageExtension = Path.GetExtension(file.FileName);

					var file2 = new
					{
						Content = memoryStream.ToArray(),
					};

					string base64string = Convert.ToBase64String(file2.Content);

					var user = _userManager.GetUserAsync(User);

					user.Result.ImageUrl = "data:image/" + imageExtension.Replace(".","").ToString() + ";base64," + base64string;

					await _userManager.UpdateAsync(user.Result);
				}

				

				

				//Eski sistem

				//string imageExtension = Path.GetExtension(file.FileName);

				//string imageName = Guid.NewGuid() + imageExtension;

				//string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/images/{imageName}");

				//using var stream = new FileStream(path, FileMode.Create);

				//await file.CopyToAsync(stream);
			}

			return RedirectToAction("UserEditProfile");
		}
	}
}
