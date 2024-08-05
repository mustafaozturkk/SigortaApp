using AspNetCoreHero.ToastNotification.Abstractions;
using SigortaApp.BL.Abstract;
using SigortaApp.BL.Concrete;
using SigortaApp.Entity.Dto;
using SigortaApp.DAL.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace SigortaApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PaymentTaskController : Controller
    {
        private readonly IPaymentTaskService _paymentTaskService;
        private readonly ITaskService _taskService;
        private readonly Context _context;
        private readonly INotyfService _notyfService;

        public PaymentTaskController(IPaymentTaskService paymentTaskService, Context context, INotyfService notyfService, ITaskService taskService)
        {
            _paymentTaskService = paymentTaskService;
            _context = context;
            _notyfService = notyfService;
            _taskService = taskService;
        }

        [Authorize(Roles ="Admin")]
        public IActionResult Index()
        {
            var values = _paymentTaskService.GetListWithCompany();
            return View(values);
        }

        public IActionResult GetPaid(TaskDto task)
        {

            var payment = _paymentTaskService.GetList().Where(w => w.TaskId == task.Id).First();
            payment.PaymentReceived = true;
            _paymentTaskService.TUpdate(payment);
            _notyfService.Success("İşem Başarılı bir şekilde tamamlandı.");
            return RedirectToAction("Index","Task");
        }

        public IActionResult GetPaidById(int Id)
        {

            var payment = _paymentTaskService.GetList().Where(w => w.Id == Id).First();
            payment.PaymentReceived = true;
            _paymentTaskService.TUpdate(payment);
            _notyfService.Success("İşem Başarılı bir şekilde tamamlandı.");
            return RedirectToAction("Index", "PaymentTask");
        }
    }
}
