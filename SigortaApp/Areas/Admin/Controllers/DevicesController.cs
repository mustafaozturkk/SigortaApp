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
    public class DevicesController : Controller
    {
        private readonly IDevicesService _devicesService;
        private readonly IBrandService _brandService;
        private readonly ITypesService _typesService;
        private readonly ICategoryService _categoryService;
        private readonly INotyfService _notyfService;
        private readonly Context _context;

        public DevicesController(IDevicesService devicesService, IBrandService brandService, ITypesService typesService, ICategoryService categoryService, INotyfService notyfService, Context context)
        {
            _devicesService = devicesService;
            _brandService = brandService;
            _typesService = typesService;
            _categoryService = categoryService;
            _notyfService = notyfService;
            _context = context;
        }

        [Authorize(Roles ="Admin")]
        public IActionResult Index(int page=1)
        {
            BrandAndTypesList();
            var values = _devicesService.GetListWithBrandAndTypes().Where(w => w.IsActive == true).GroupBy(g => g.TypesId).Select(s => s.ToList()).ToPagedList(page,10);
            return View(values.ToPagedList(page,10));
        }

        [HttpPost]
        public IActionResult AddDevices(DevicesUsersDto p)
        {
            var username = User.Identity.Name;
            var user = _context.Users.Where(x => x.UserName == username).FirstOrDefault();
            var devicemodel = new Devices
            {
                Brand = p.Brand,
                BrandId = p.BrandId,
                Category = p.Category,
                CategoryId = p.CategoryId,
                CupboardNumber = p.CupboardNumber,
                Description = p.Description,
                Id = p.Id,
                IsActive =p.IsActive,
                SerialNumber = p.SerialNumber,
                Types = p.Types,
                TypesId = p.TypesId                
            };
            if (p.BrandId != 0 && p.TypesId != 0)
            {
                if (p.Id != 0)
                {
                    var lastDevice = _devicesService.TGetById(p.Id);
                    devicemodel.CreatedDate = lastDevice.CreatedDate;
                    devicemodel.CreatedBy = lastDevice.CreatedBy;
                    devicemodel.IsActive = true;
                    _devicesService.TUpdate(devicemodel);
                    _notyfService.Success("İşem Başarılı bir şekilde tamamlandı.");
                }
                else
                {
                    string[] serialNumebers = {};
                    if (p.SerialNumber != null)
                    {
                       serialNumebers = p.SerialNumber.Replace("\r\n", ",").Replace(" ", "").Split(",");
                    }
                    //p.SerialNumber.Replace("\r\n", ",").Replace(" ", "").Split(",");
                    var count = 0;
                    if (serialNumebers.Count() > 0)
                    {
                        foreach (var item in serialNumebers)
                        {
                            if (item != "")
                            {
                                count++;
                                devicemodel.Id = 0;
                                devicemodel.CreatedDate = DateTime.Now;
                                devicemodel.CreatedBy = user.Id;
                                devicemodel.SerialNumber = item;
                                devicemodel.IsActive = true;
                                _devicesService.TAdd(devicemodel);
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < p.Number; i++)
                        {
                            count++;
                            devicemodel.Id = 0;
                            devicemodel.CreatedDate = DateTime.Now;
                            devicemodel.CreatedBy = user.Id;
                            devicemodel.SerialNumber = "";
                            devicemodel.IsActive = true;
                            _devicesService.TAdd(devicemodel);
                        }
                    }
                    
                    _notyfService.Success("İşem Başarılı bir şekilde tamamlandı.(" + count + " tane ürün eklenmiştir.)");
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

        public IActionResult TypesByBrand(int brandId, int categoryId)
        {
            List<SelectListItem> typesValues = (from x in _typesService.GetList()
                                                where x.BrandId == brandId && x.CategoryId == categoryId
                                                select new SelectListItem
                                                {
                                                    Text = x.Name,
                                                    Value = x.Id.ToString()
                                                }).ToList();
            return Json(typesValues);
        }

        public IActionResult UpdateDevices(int id)
        {
            var result = _devicesService.TGetById(id);
            return Json(result);
        }

        public IActionResult DeleteDevice(int id)
        {
            var device = _devicesService.TGetById(id);
            if (device != null)
            {
                _devicesService.TDelete(device);
                _notyfService.Success("İşem Başarılı bir şekilde tamamlandı.");
                return RedirectToAction("Index");
            }
            else
            {
                _notyfService.Error("Hata! Eklenemedi!");
                return RedirectToAction("Index");
            }
             
        }

        public IActionResult GetChildList(int id, int page = 1)
        {
            BrandAndTypesList();
            var result = _devicesService.GetListWithBrandAndTypes().Where(w => w.IsActive == true && w.TypesId ==id).ToPagedList(page, 10);
            return View(result);
        }
    }
}
