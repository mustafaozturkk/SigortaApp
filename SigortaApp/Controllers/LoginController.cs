using AspNetCoreHero.ToastNotification.Abstractions;
using SigortaApp.Entity.Concrete;
using SigortaApp.Web.Models;
using SigortaApp.DAL.Concrete;
using DocumentFormat.OpenXml.ExtendedProperties;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace SigortaApp.Web.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;//DEVAM EDİLECEK TO DO!
        private readonly INotyfService _notyfService;

        public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager,INotyfService notyfService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _notyfService = notyfService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserSignInViewModel p)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(p.UserName, p.Password, false/*Çerezlerde hatırlasın mı ?*/, true/*5 yanlış girişte ban yer kullanıcı*/);
                var user = await _userManager.FindByNameAsync(p.UserName);
                var roles = await _userManager.GetRolesAsync(user);
                ViewBag.roles = roles;
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    return RedirectToAction("Index","Login");
                }
            }
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index2", "Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
        
        public IActionResult Index2()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index2(UserSignInViewModel p)
        {
            //var user = await _userManager.FindByNameAsync(p.UserName);
            //var passwordValidator = new PasswordValidator<AppUser>();
            //var result = passwordValidator.ValidateAsync(_userManager, user, p.Password);
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(p.UserName);
                var claims = await _userManager.GetClaimsAsync(user);
                if (claims.Count == 0)
                {
                    var aa2 = await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.GivenName, user.NameSurname));
                    var aa21 = await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.PrimarySid, user.Id.ToString()));
                }
                var result = await _signInManager.PasswordSignInAsync(p.UserName, p.Password, false/*Çerezlerde hatırlasın mı ?*/, true/*5 yanlış girişte ban yer kullanıcı*/);
                var roles = await _userManager.GetRolesAsync(user);


                ViewBag.roles = roles;
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                else
                {
                    _notyfService.Error("Kullanıcı adı veya şifreniz hatalı!");
                    //return RedirectToAction("Index2", "Login");
                }
            }
            return View();
        }
    }
}
