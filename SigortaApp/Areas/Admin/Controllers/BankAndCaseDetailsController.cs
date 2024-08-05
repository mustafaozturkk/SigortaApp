using SigortaApp.BL.Abstract;
using SigortaApp.Entity.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using SigortaApp.Entity.Dto;
using Microsoft.AspNetCore.Authorization;

namespace SigortaApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BankAndCaseDetailsController : Controller
    {
        private readonly IBankAndCaseDetailsService _bankAndCaseDetailsService;

        public BankAndCaseDetailsController(IBankAndCaseDetailsService bankAndCaseDetailsService)
        {
            _bankAndCaseDetailsService = bankAndCaseDetailsService;
        }

        [Authorize(Roles ="Admin")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddBankAndCaseDetails(BankAndCaseDetailsDto bankAndCaseDetailsDto)
        {
            if (bankAndCaseDetailsDto != null)
            {
                if (bankAndCaseDetailsDto.Id != 0)
                {
                    BankAndCaseDetails bankAndCaseDetails = new BankAndCaseDetails
                    {
                        BankId = bankAndCaseDetailsDto.BankId,
                        CaseId = bankAndCaseDetailsDto.CaseId,
                        CreatedBy = Convert.ToInt32(User.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value),
                        CreatedDate = DateTime.Now,
                        Date = bankAndCaseDetailsDto.Date,
                        Description = bankAndCaseDetailsDto.Description,
                        Id = bankAndCaseDetailsDto.Id,
                        IsIn = bankAndCaseDetailsDto.IsIn,
                        PaymentMethodId = bankAndCaseDetailsDto.PaymentMethodId,
                        Price = bankAndCaseDetailsDto.Price,
                    };
                    //_bankAndCaseDetailsService.TUpdate(bankAndCaseDetails);
                }
                else
                {
                    if (bankAndCaseDetailsDto.IsVirman)
                    {
                        if (bankAndCaseDetailsDto.CaseId != null)
                        {
                            if (bankAndCaseDetailsDto.BankVirman == null && (bankAndCaseDetailsDto.CaseVirman == null || bankAndCaseDetailsDto.CaseVirman == bankAndCaseDetailsDto.CaseId))
                            {
                                return View();
                            }
                        }
                        else if (bankAndCaseDetailsDto.BankId != null)
                        {
                            if (bankAndCaseDetailsDto.CaseVirman == null && (bankAndCaseDetailsDto.BankVirman == null || bankAndCaseDetailsDto.BankVirman == bankAndCaseDetailsDto.BankId))
                            {
                                return View();
                            }
                        }
                    }

                    BankAndCaseDetails bankAndCaseDetails = new BankAndCaseDetails
                    {
                        BankId = bankAndCaseDetailsDto.BankId,
                        CaseId = bankAndCaseDetailsDto.CaseId,
                        CreatedBy = Convert.ToInt32(User.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value),
                        CreatedDate = DateTime.Now,
                        Date = bankAndCaseDetailsDto.Date,
                        Description = bankAndCaseDetailsDto.Description,
                        Id = 0,
                        IsIn = bankAndCaseDetailsDto.IsIn,
                        PaymentMethodId = bankAndCaseDetailsDto.PaymentMethodId,
                        Price = bankAndCaseDetailsDto.Price,
                    };
                    _bankAndCaseDetailsService.TAdd(bankAndCaseDetails);

                    if (bankAndCaseDetailsDto.IsVirman && (bankAndCaseDetailsDto.BankVirman != null || bankAndCaseDetailsDto.CaseVirman != null))
                    {
                        BankAndCaseDetails bankAndCaseDetails2 = new BankAndCaseDetails
                        {
                            BankId = bankAndCaseDetailsDto.BankVirman,
                            CaseId = bankAndCaseDetailsDto.CaseVirman,
                            CreatedBy = Convert.ToInt32(User.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value),
                            CreatedDate = DateTime.Now,
                            Date = bankAndCaseDetailsDto.Date,
                            Description = bankAndCaseDetailsDto.BankId != null ? bankAndCaseDetailsDto.BankId + " -> " : bankAndCaseDetailsDto.CaseId + " -> " + bankAndCaseDetailsDto.BankVirman != null ? bankAndCaseDetailsDto.BankVirman + " Virman" : bankAndCaseDetailsDto.CaseVirman + " Virman",
                            Id = 0,
                            IsIn = !bankAndCaseDetailsDto.IsIn,
                            PaymentMethodId = bankAndCaseDetailsDto.PaymentMethodId,
                            Price = bankAndCaseDetailsDto.Price,
                        };
                        _bankAndCaseDetailsService.TAdd(bankAndCaseDetails2);
                    }
                }
            }

            return View();
        }
    }
}
