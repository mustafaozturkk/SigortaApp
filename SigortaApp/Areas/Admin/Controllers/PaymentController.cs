using AspNetCoreHero.ToastNotification.Abstractions;
using SigortaApp.BL.Abstract;
using SigortaApp.Entity.Concrete;
using SigortaApp.Entity.Dto;
using SigortaApp.Entity.Enums;
using SigortaApp.DAL.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace SigortaApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly ITaskService _taskService;
        private readonly Context _context;
        private readonly INotyfService _notyfService;

        public PaymentController(IPaymentService paymentService, ITaskService taskService, Context context, INotyfService notyfService)
        {
            _paymentService = paymentService;
            _taskService = taskService;
            _context = context;
            _notyfService = notyfService;
        }

        [Authorize(Roles ="Admin")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPayment(Payment payment)
        {
            if (payment != null)
            {
                payment.PaymentMethodId = ((payment.StatusId == (int)OdemeTuruEnum.Borclandirma) || (payment.StatusId == (int)OdemeTuruEnum.Alacaklandirma)) ? (int)OdemeTuruEnum.AcikHesap : payment.PaymentMethodId;
                if (payment.Id != 0)
                {
                    _paymentService.TUpdate(payment);
                }
                else
                {
                    payment.CreatedBy = Convert.ToInt32(User.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value);
                    _paymentService.TAdd(payment);
                }
            }
            return RedirectToAction("GetCompanyDetails","Company", new { Id = payment.CompanyId });
        }

        public IActionResult GetPaymentById(int paymentId)
        {
            var result = _paymentService.TGetById(paymentId);
            return Json(result);
        }

        public IActionResult DeletePayment(int paymentId)
        {
            if (paymentId != 0)
            {
                var payment = _paymentService.TGetById(paymentId);
                _paymentService.TDelete(payment);
                return RedirectToAction("GetCompanyDetails", "Company", new { Id = payment.CompanyId });
            }
            else
            {
                return Json(false);
            }
        }
    }
}
