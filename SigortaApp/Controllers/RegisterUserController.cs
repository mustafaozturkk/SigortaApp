using SigortaApp.Entity.Concrete;
using SigortaApp.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using SendGrid.Helpers.Mail;
using SendGrid;
using System;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json.Linq;
using System.Text;
using SigortaApp.Entity.Enums;
using SigortaApp.BL.Abstract;
using SigortaApp.BL.Concrete;
using SigortaApp.Entity;
using RestSharp;
using System.Linq;
using AspNetCoreHero.ToastNotification.Notyf;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace SigortaApp.Web.Controllers
{
    [AllowAnonymous]
    public class RegisterUserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ICompanyService _companyService;
        private readonly INotyfService _notyfService;

        public RegisterUserController(UserManager<AppUser> userManager, ICompanyService companyService, INotyfService notyfService)
        {
            _userManager = userManager;
            _companyService = companyService;
            _notyfService = notyfService;

        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserSignUpViewModel p)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AppUser user = new AppUser()
                    {
                        Email = p.Mail,
                        UserName = p.UserName,
                        NameSurname = p.NameSurname,
                        PhoneNumber = p.PhoneNumber
                    };

                    var result = await _userManager.CreateAsync(user, p.Password);
                    var result2 = await _userManager.AddToRoleAsync(user, "KULLANICI");

                    Company company = new Company
                    {
                        Name = p.NameSurname,
                        Email = p.Mail,
                        MobilePhone = p.PhoneNumber,
                        Phone = p.PhoneNumber,
                        IsActive = true,
                        PersonId = user.Id,
                        Title = p.UserName,
                        CurrentGroupId = p.IsPersonType == (int)RegisterTypeEnum.Personel ? (int)RegisterTypeEnum.Personel : (int)RegisterTypeEnum.Sirket,
                    };

                    _companyService.TAdd(company);

                    Whatsapp(user, WhatsappEnum.KayitYapildi);
                    

                    if (result.Succeeded)//&& result2.Succeeded
                    {
                        return RedirectToAction("Index2", "Login");
                    }
                }
                catch (Exception ex)
                {
                    _notyfService.Error("İşem Başarısız oldu. Bilgilerinizi gözden geçirip tekrar deneyin.");
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(p);
        }

        [HttpGet]
        public IActionResult PasswordReset()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PasswordReset(ResetPasswordViewModel model)
        {
            //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
            AppUser user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(resetToken);
                var codeEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);

                MailMessage mail = new MailMessage();
                mail.IsBodyHtml = true;
                mail.To.Add(user.Email);
                mail.From = new MailAddress("mustafa@tuga.com.tr", "Şifre Güncelleme", System.Text.Encoding.UTF8);
                mail.Subject = "Şifre Güncelleme Talebi";
                mail.Body = $"<a target=\"_blank\" href=\"https://localhost:44389{Url.Action("UpdatePassword", "RegisterUser", new { userId = user.Id, token = HttpUtility.UrlEncode(codeEncoded) })}\">Yeni şifre talebi için tıklayınız</a>";
                mail.IsBodyHtml = true;
                SmtpClient smp = new SmtpClient();
                smp.Credentials = new NetworkCredential("mustafa@tuga.com.tr", "2001MOmo2001");
                smp.Port = 587;
                smp.Host = "smtp.yandex.com";
                smp.EnableSsl = true;
                smp.UseDefaultCredentials = false;

                smp.Send(mail);

                //var apiKey = "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";
                //var client = new SendGridClient(apiKey);
                //var from = new EmailAddress("mustafa@tuga.com.tr", "mustafa ozturk");
                //var subject = "Sending with SendGrid is Fun";
                //var to = new EmailAddress("ozturk.musti65@gmail.com", "mustafa öztürk");
                //var plainTextContent = "and easy to do anywhere, even with C#";
                //var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
                //var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                //var response = await client.SendEmailAsync(msg);


                ViewBag.State = true;
            }
            else
                ViewBag.State = false;

            return View();
        }


        [HttpGet("[controller]/[action]/{userID}/{Token}")]
        public IActionResult UpdatePassword(string userId, string token)
        {
            return View();
        }


        [HttpPost("[controller]/[action]/{userID}/{Token}")]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordViewModel model, string userId, string token)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            var codeDecodedBytes = WebEncoders.Base64UrlDecode(token);
            var codeDecoded = Encoding.UTF8.GetString(codeDecodedBytes);

            IdentityResult result = await _userManager.ResetPasswordAsync(user, codeDecoded, model.Password);
            if (result.Succeeded)
            {
                ViewBag.State = true;
                await _userManager.UpdateSecurityStampAsync(user);
            }
            else
                ViewBag.State = false;
            return View();
        }

        public async void Whatsapp(AppUser user, WhatsappEnum whatsappEnum)
        {
            var url = "https://api.ultramsg.com/instance72775/messages/chat";
            var client = new RestClient();
            var request = new RestRequest();
            if ((int)whatsappEnum == (int)WhatsappEnum.KayitYapildi)
            {
                    client = new RestClient(url);
                    request = new RestRequest(url, Method.Post);
                    request.AddHeader("content-type", "application/x-www-form-urlencoded");
                    request.AddParameter("token", "m6kw882zxd4or0um");
                    request.AddParameter("to", "+9" + user.PhoneNumber);
                    request.AddParameter("body", "Sayın " + user.NameSurname + "\nTakım Yıldızı Otomasyon sistemine başarılı şekilde kayıt oldunuz. Sizleri aramızda görmekten mutluluk duyarız. \nTakım Yıldızı Lojistik İyi Çalışmalar Diler");
                    RestResponse response = await client.ExecuteAsync(request);
                    var output = response.Content;
            }
        }
    }
}
