using SigortaApp.BL.Abstract;
using SigortaApp.BL.Concrete;
using SigortaApp.Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SigortaApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BankAccountController : Controller
    {
        private readonly IBankAccountService _bankAccountService;
        private readonly IUnitService _unitService;

        public BankAccountController(IBankAccountService bankAccountService, IUnitService unitService)
        {
            _bankAccountService = bankAccountService;
            _unitService = unitService;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var result = _bankAccountService.GetList();
            return View(result);
        }

        public IActionResult Details(int Id)
        {
            UnitUst();
            var result = _bankAccountService.GetBankAccountsWithPayment(Id);
            return View(result);
        }

        public void UnitUst()
        {
            List<SelectListItem> UnitValues = (from x in _unitService.GetList()
                                               where x.UnitTypesId == 2 && (x.Id == (int)SigortaApp.Entity.Enums.OdemeTuruEnum.Havale || x.Id == (int)SigortaApp.Entity.Enums.OdemeTuruEnum.KrediKartı)
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

        public IActionResult AddBankAccount(BankAccount bankAccount)
        {
            if (bankAccount != null)
            {
                if (bankAccount.Id != 0)
                {
                    _bankAccountService.TUpdate(bankAccount);
                }
                else
                {
                    _bankAccountService.TAdd(bankAccount);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult GetBankAccount(int bankAccountId)
        {
            var result = _bankAccountService.TGetById(bankAccountId);
            return Json(result);
        }

        [HttpPost]
        public IActionResult DeleteBankAccount(int bankAccountId)
        {
            var bankAccount = _bankAccountService.TGetById(bankAccountId);
            try
            {
                _bankAccountService.TDelete(bankAccount);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return Json(false);
            }

        }
    }
}
