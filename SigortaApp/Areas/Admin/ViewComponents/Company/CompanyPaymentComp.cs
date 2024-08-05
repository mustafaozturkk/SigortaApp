using SigortaApp.BL.Abstract;
using SigortaApp.BL.Concrete;
using SigortaApp.DAL.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Xml.Linq;

namespace SigortaApp.Web.Areas.Admin.ViewComponents.Company
{
    public class CompanyPaymentComp : ViewComponent
    {
        private readonly Context _context;
        private readonly IPaymentService _paymentService;
        private readonly ICompanyService _companyService;

        public CompanyPaymentComp(Context context, IPaymentService paymentService, ICompanyService companyService)
        {
            _context = context;
            _paymentService = paymentService;
            _companyService = companyService;
        }

        public IViewComponentResult Invoke(int Id)
        {
            var result = _companyService.GetCompanyWithPaymentByCompanyId(Id);
            return View(result);
        }
    }
}
