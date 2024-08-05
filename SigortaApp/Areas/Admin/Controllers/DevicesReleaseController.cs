using AspNetCoreHero.ToastNotification.Abstractions;
using SigortaApp.BL.Abstract;
using SigortaApp.BL.Concrete;
using SigortaApp.Entity.Concrete;
using SigortaApp.Entity.Dto;
using SigortaApp.DAL.Concrete;
using SigortaApp.DAL.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace SigortaApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DevicesReleaseController : Controller
    {
        private readonly IDevicesReleaseService _devicesReleaseService;
        private readonly IDevicesService _devicesService;
        private readonly INotyfService _notyfService;
        private readonly IUnitService _unitService;
        private readonly Context _context;
        private readonly IBrandService _brandService;
        private readonly ICategoryService _categoryService;
        private readonly ITypesService _typesService;


        public DevicesReleaseController(IDevicesReleaseService devicesReleaseService, IDevicesService devicesService, INotyfService notyfService, IUnitService unitService, Context context, IBrandService brandService, ICategoryService categoryService, ITypesService typesService)
        {
            _devicesReleaseService = devicesReleaseService;
            _devicesService = devicesService;
            _notyfService = notyfService;
            _unitService = unitService;
            _context = context;
            _brandService = brandService;
            _categoryService = categoryService;
            _typesService = typesService;
        }

        [Authorize(Roles ="Admin")]
        public IActionResult Index(int page = 1)
        {
            UnitUst();
            BrandAndTypesList();
            var result = _devicesReleaseService.GetListWithDevices().ToPagedList(page, 10);
            return View(result);
        }

        public IActionResult Cities(int ustid)
        {
            List<SelectListItem> UnitValues = (from x in _unitService.GetList()
                                               where x.UstId == ustid
                                               select new SelectListItem
                                               {
                                                   Text = x.Name,
                                                   Value = x.Id.ToString()
                                               }).ToList();
            return Json(UnitValues);
        }

        public void UnitUst()
        {
            List<SelectListItem> UnitUstValues = (from x in _unitService.GetList()
                                                  where x.UstId == null
                                                  select new SelectListItem
                                                  {
                                                      Text = x.Name,
                                                      Value = x.Id.ToString()
                                                  }).ToList();
            ViewBag.uuv = UnitUstValues;
        }

        [HttpPost]
        public IActionResult AddDevicesRelease(DeviceReleaseDto p)
        {
            var username = User.Identity.Name;
            var user = _context.Users.Where(x => x.UserName == username).FirstOrDefault();
            if (p.GoingToUnit != 0 && p.SerialNumber != "")
            {
                if (p.Id != 0)
                {
                    //var lastDevice = devicesManager.TGetById(p.Id);
                    //p.CreatedDate = lastDevice.CreatedDate;
                    //p.CreatedBy = lastDevice.CreatedBy;
                    //devicesManager.TUpdate(p);
                    //_notyfService.Success("İşem Başarılı bir şekilde tamamlandı.");
                }
                else
                {
                    if (p.SerialNumber != null)
                    {
                        var serialNumebers = p.SerialNumber.Replace("\r\n", ",").Replace(" ", "").Split(",");
                        var count = 0;
                        foreach (var item in serialNumebers)
                        {
                            var device = _devicesService.GetList().Where(w => w.SerialNumber == item).FirstOrDefault();
                            if (item != "" && device != null)
                            {
                                DevicesRelease d = new DevicesRelease();
                                count++;
                                d.CreatedDate = DateTime.Now;
                                d.CreatedBy = user.Id;
                                d.DeviceId = device.Id;
                                d.GoingToUnit = p.GoingToUnit;
                                d.Description = p.Description;
                                d.ShippingPostCode = p.ShippingPostCode;
                                //_devicesReleaseManager.TAdd(d);//AÇILACAKKKKKK

                                device.IsActive = false;
                                //_devicesManager.TUpdate(device);//AÇILACAKKKKKK
                            }
                            else
                            {
                                _notyfService.Error("Hata! Eklenemedi! Lütfen Geçerli bir seri numarası giriniz!! '" + item + "' seri numarası depoda yok!!");
                                return RedirectToAction("Index");
                            }
                        }
                    }
                    else
                    {
                        var device = _devicesService.GetList().Where(w => w.IsActive == true).FirstOrDefault();
                        for (int i = 0; i < p.Number; i++)
                        {

                        }
                    }
                    //_notyfService.Success("İşem Başarılı bir şekilde tamamlandı.(" + count + " tane ürün eklenmiştir.)");
                }
                return RedirectToAction("Index");
            }
            else
            {
                _notyfService.Error("Hata! Eklenemedi!");
                return RedirectToAction("Index");
            }
        }

        public void BrandAndTypesList()
        {
            List<SelectListItem> brandValues = (from x in _brandService.GetList()
                                                select new SelectListItem
                                                {
                                                    Text = x.Name,
                                                    Value = x.Id.ToString()
                                                }).ToList();

            List<SelectListItem> typesValues = (from x in _typesService.GetList()
                                                select new SelectListItem
                                                {
                                                    Text = x.Name,
                                                    Value = x.Id.ToString()
                                                }).ToList();
            List<SelectListItem> categoryValues = (from x in _categoryService.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.Name,
                                                       Value = x.Id.ToString()
                                                   }).ToList();
            ViewBag.bv = brandValues;
            ViewBag.tv = typesValues;
            ViewBag.cv = categoryValues;
        }
    }
}
