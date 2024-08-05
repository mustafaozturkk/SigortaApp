using SigortaApp.BL.Abstract;
using SigortaApp.BL.Concrete;
using SigortaApp.Entity.Concrete;
using SigortaApp.Entity.Dto;
using SigortaApp.Entity.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SigortaApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CaseController : Controller
    {
        private readonly IUnitService _unitService;
        private readonly IUnitTypesService _unitTypesService;
        private readonly IBankAccountService _bankAccountService;
        private readonly IBankAndCaseDetailsService _bankAndCaseDetailsService;

        public CaseController(IUnitService unitService, IUnitTypesService unitTypesService, IBankAccountService bankAccountService, IBankAndCaseDetailsService bankAndCaseDetailsService)
        {
            _unitService = unitService;
            _unitTypesService = unitTypesService;
            _bankAccountService = bankAccountService;
            _bankAndCaseDetailsService = bankAndCaseDetailsService;
        }

        [Authorize(Roles ="Admin")]
        public IActionResult Index()
        {
            var result = _unitService.GetCases();
            return View(result);
        }

        public IActionResult Details(int Id)
        {
            UnitUst();
            var result = _bankAndCaseDetailsService.GetCaseWithPayment(Id);
            return View(result);
        }

        public void UnitUst()
        {
            List<SelectListItem> UnitValues = (from x in _unitService.GetList()
                                               where x.UnitTypesId == 2 && (x.Id == (int)SigortaApp.Entity.Enums.OdemeTuruEnum.Nakit)
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
            ViewBag.uv = UnitValues;
            ViewBag.bav = BankAccountValues;
            ViewBag.cas = CaseValues;
        }

        public IActionResult AddCase(CaseDto cas)
        {
            if (cas != null)
            {
                if (cas.Id != 0)
                {
                    _unitService.TUpdate(cas);
                }
                else
                {
                    cas.UnitTypesId = (int)UnitTypesEnum.KASALAR;
                    _unitService.TAdd(cas);

                    BankAndCaseDetails bankAndCaseDetails = new BankAndCaseDetails{
                        CaseId = cas.Id,
                        IsIn = true,
                        Price = cas.AccountPrice,
                        CreatedBy = Convert.ToInt32(User.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value),
                        CreatedDate = DateTime.Now,
                        Description = "İlk Hesap Girişi"
                    };

                    _bankAndCaseDetailsService.TAdd(bankAndCaseDetails);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult GetCase(int caseId)
        {
            CaseDto caseDto = new CaseDto();
            var result = _unitService.TGetById(caseId);
            var account = _bankAndCaseDetailsService.GetCaseWithPayment(caseId);
            caseDto.Id = result.Id;
            caseDto.Name = result.Name;
            caseDto.AccountPrice = account.AccountPrice;
            return Json(caseDto);
        }

        [HttpPost]
        public IActionResult DeleteCase(int caseId)
        {
            var casee = _unitService.TGetById(caseId);
            try
            {
                _unitService.TDelete(casee);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return Json(false);
            }

        }
    }
}
