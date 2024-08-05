using AspNetCoreHero.ToastNotification.Abstractions;
using SigortaApp.BL.Abstract;
using SigortaApp.BL.Concrete;
using SigortaApp.Entity.Concrete;
using SigortaApp.Entity.Dto;
using SigortaApp.Entity.Enums;
using SigortaApp.DAL.Concrete;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SigortaApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : BaseController<Company>
    {
        private readonly Context _context;
        private readonly ICompanyService _companyservice;
        private readonly INotyfService _notyfService;
        private readonly IUserService _userService;
        private readonly IUnitService _unitService;
        private readonly IBankAccountService _bankAccountService;
        private readonly IPaymentService _paymentService;

        public CompanyController(Context context, ICompanyService companyservice, INotyfService notyfService, IUserService userService, IUnitService unitService, IBankAccountService bankAccountService, IPaymentService paymentService)
        {
            _context = context;
            _companyservice = companyservice;
            _notyfService = notyfService;
            _userService = userService;
            _unitService = unitService;
            _bankAccountService = bankAccountService;
            _paymentService = paymentService;
        }

        [Authorize(Roles ="Admin")]
        public IActionResult Index()
        {
            List<SelectListItem> UserValues = (from x in _userService.GetList()
                                               select new SelectListItem
                                               {
                                                   Text = x.NameSurname,
                                                   Value = x.Id.ToString()
                                               }).ToList();
            List<SelectListItem> CariGroup = (from x in _unitService.GetList()
                                              where x.UnitTypesId == (int)UnitTypesEnum.CariGrup
                                              select new SelectListItem
                                              {
                                                  Text = x.Name,
                                                  Value = x.Id.ToString()
                                              }).ToList();
            ViewBag.userval = UserValues;
            ViewBag.carigrup = CariGroup;
            return View();
        }

        [HttpPost]
        public IActionResult AddCompany(Company company)
        {
            if (company.Id != 0)
            {
                var oldcompany = _companyservice.TGetById(company.Id);
                company.CreatedDate = oldcompany.CreatedDate;
                company.CreatedBy = oldcompany.CreatedBy;
                _companyservice.TUpdate(company);
            }
            else
            {
                var isCompany = _companyservice.GetList().Where(w => w.Name.ToLower() == company.Name.ToLower()).Any();
                if (!isCompany)
                {
                    company.CreatedBy = Convert.ToInt32(User.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value);
                    company.CreatedDate = DateTime.Now;
                    _companyservice.TAdd(company);
                }
                else
                {
                    _notyfService.Error("Bu şirket adı sistemde kayıtlıdır.");
                    return RedirectToAction("Index");
                }
                
            }
            _notyfService.Success("İşem Başarılı bir şekilde tamamlandı.");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult GetCompany(int companyId)
        {
            var result = _companyservice.TGetById(companyId);
            return Json(result);
        }

        [HttpPost]
        public IActionResult DeleteCompany(int companyId)
        {
            var company = _companyservice.TGetById(companyId);
            try
            {
                _companyservice.TDelete(company);
                _notyfService.Success("İşem Başarılı bir şekilde tamamlandı.");
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                _notyfService.Success("İşem Başarısız Oldu.");
                return Json(false);
            }
        }

        public IActionResult GetCompanyDetails(int Id)
        {
            //var satis = _devicesReleaseService.GetListWithDevices().Where(w => w.CompanyId == id).ToList();
            var result = _companyservice.GetCompanyWithPaymentByCompanyId(Id);
            ViewBag.CompanyDetails = result;
            if (result.Count == 0)
            {
                result.Add(new PaymentDto { Company = _companyservice.TGetById(Id), CurrentGroupName = _unitService.TGetById(_companyservice.TGetById(Id).CurrentGroupId).Name });
            }
            //ViewBag.KalanBorc = satis.Sum(x => x.TotalRemainingDebt);
            //ViewBag.ToplamBorc = satis.Sum(s => s.TotalAmount);
            //ViewBag.Odenen = ViewBag.ToplamBorc - ViewBag.KalanBorc;
            UnitUst();
            return View(result);
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
            List<SelectListItem> UnitValues = (from x in _unitService.GetList()
                                               where x.UnitTypesId == 2
                                               select new SelectListItem
                                               {
                                                   Text = x.Name,
                                                   Value = x.Id.ToString()
                                               }).ToList();
            List<SelectListItem> BankAccountValues = (from x in _bankAccountService.GetList()
                                                      select new SelectListItem
                                                      {
                                                          Text = x.Name,
                                                          Value = x.Id.ToString()
                                                      }).ToList();
            List<SelectListItem> CaseValues = (from x in _unitService.GetList()
                                               where x.UnitTypesId == 3
                                               select new SelectListItem
                                               {
                                                   Text = x.Name,
                                                   Value = x.Id.ToString()
                                               }).ToList();
            ViewBag.uuv = UnitUstValues;
            ViewBag.uv = UnitValues;
            ViewBag.bav = BankAccountValues;
            ViewBag.cas = CaseValues;
        }

        [HttpGet]
        public IActionResult GetTaskComponent(int companyId)
        {
            return ViewComponent("CompanyTaskComp", new { Id = companyId });//it will call Follower.cs InvokeAsync, and pass id to it.
        }

        public IActionResult GetCompanyRapor(int Id)
        {
            var pay = _paymentService.GetListByCompanyId(Id).ToList();
            var asd = _companyservice.GetList().First();
            return View(pay);
        }

        public IActionResult PrintPdf(string html)
        {
            return View();
        }
        
        [AllowAnonymous]
        public IActionResult DownloadPDFFile(int Id)
        {
            var str = "Admin/Company/GetCompanyRapor/" + Id;
            var pdf = new GeneratePDF($"https://{Request.Host.Value}/{str}");
            var pdfFile = pdf.GetPdf();

            var pdfStream = new MemoryStream(pdfFile);
            return new FileStreamResult(pdfStream, "application/pdf");
        }

        public IActionResult GetData()
        {
            var companyList = _companyservice.GetList().Where(w => w.IsActive).ToList();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            if (searchValue != null)
            {
                companyList = companyList.Where(w => w.Name.ToLower().Contains(searchValue.ToLower())).ToList();
            }
            var asd = LoadData(companyList);

            return Json(asd.Value);
        }
    }
}
