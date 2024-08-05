using AspNetCoreHero.ToastNotification.Abstractions;
using SigortaApp.BL.Abstract;
using SigortaApp.BL.Concrete;
using SigortaApp.Entity.Concrete;
using SigortaApp.Web.Areas.Admin.Models;
using SigortaApp.DAL.Abstract;
using SigortaApp.DAL.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace SigortaApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TypesController : Controller
    {
        private readonly INotyfService _notyfService;
        private readonly ITypesService _typesService;
        private readonly IBrandService _brandService;
        private readonly IUnitOfWork _uow;

        public TypesController(INotyfService notyfService, ITypesService typesService, IBrandService brandService,IUnitOfWork uow)
        {
            _typesService = typesService;
            _brandService = brandService;
            _notyfService = notyfService;
            _uow = uow;
        }

        [Authorize(Roles ="Admin")]
        public IActionResult Index(int page=1)
        {
            BrandList();
            var values2 = _uow.typesDal.GetListWithBrand().ToPagedList(page, 10);
            return View(values2);
        }

        [HttpPost]
        public IActionResult AddTypes(Types p)
        {
            if (p.Name != null && p.Name != "" && p.BrandId != 0 )
            {
                if (p.Id != 0)
                    _typesService.TUpdate(p);
                else
                    _typesService.TAdd(p);
                
                _notyfService.Success("İşem Başarılı bir şekilde tamamlandı.");
                return RedirectToAction("Index");
            }
            else
            {
                _notyfService.Error("Hata! Eklenemedi!");
                return RedirectToAction("Index");
            }
        }
        public void BrandList()
        {
            List<SelectListItem> brandValues = (from x in _brandService.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.Name,
                                                       Value = x.Id.ToString()
                                                   }).ToList();
            List<SelectListItem> categoryValues = (from x in _uow.categoryDal.GetListAll()
                                                select new SelectListItem
                                                {
                                                    Text = x.Name,
                                                    Value = x.Id.ToString()
                                                }).ToList();
            ViewBag.bv = brandValues;
            ViewBag.cv = categoryValues;
        }

        public IActionResult UpdateType(int id)
        {
            var result = _typesService.TGetById(id);
            return Json(result);
        }

        public IActionResult DeleteType(int id)
        {
            var device = _typesService.TGetById(id);
            if (device != null)
            {
                _typesService.TDelete(device);
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
