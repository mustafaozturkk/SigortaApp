using AspNetCoreHero.ToastNotification.Abstractions;
using SigortaApp.BL.Abstract;
using SigortaApp.BL.Concrete;
using SigortaApp.Entity.Concrete;
using SigortaApp.DAL.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace SigortaApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;
        private readonly INotyfService _notyfService;

        public BrandController(INotyfService notyfService, IBrandService brandService)
        {
            _notyfService = notyfService;
            _brandService = brandService;
        }

        [Authorize(Roles ="Admin")]
        public IActionResult Index(int page = 1)
        {
            //_notyfService.Success("You have successfully saved the data.");
            //_notyfService.Error("Exception...");
            //_notyfService.Warning("Warning...");
            //_notyfService.Information("Welcome to FoxLearn.", 5);
            //_notyfService.Custom("Custom Notification...", 10, "#B500FF", "fa fa-home");
            var values = _brandService.GetList().ToPagedList(page, 10);
            return View(values);
        }

        [HttpPost]
        public IActionResult AddBrand(Brand p)
        {
            if (p.Name != null && p.Name != "")
            {
                if (p.Id != 0)
                    _brandService.TUpdate(p);
                else
                    _brandService.TAdd(p);

                _notyfService.Success("İşem Başarılı bir şekilde tamamlandı.");
                return RedirectToAction("Index");
            }
            else
            {
                _notyfService.Error("Hata! Eklenemedi!");
                return RedirectToAction("Index");
            }
            
        }

        public IActionResult UpdateBrand(int id)
        {
            var result = _brandService.TGetById(id);
            return Json(result);
        }

        public IActionResult DeleteBrand(int id)
        {
            var device = _brandService.TGetById(id);
            if (device != null)
            {
                _brandService.TDelete(device);
                _notyfService.Success("İşem Başarılı bir şekilde tamamlandı.");
                return RedirectToAction("Index");
            }
            else
            {
                _notyfService.Error("Hata! Eklenemedi!");
                return RedirectToAction("Index");
            }

        }
    }
}
