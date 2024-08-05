using SigortaApp.BL.Abstract;
using SigortaApp.BL.Concrete;
using SigortaApp.DAL.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Xml.Linq;

namespace SigortaApp.Web.Areas.Admin.ViewComponents.Company
{
    public class CompanyTaskComp : ViewComponent
    {
        private readonly Context _context;
        private readonly ITaskService _taskService;

        public CompanyTaskComp(Context context, ITaskService taskService)
        {
            _context = context;
            _taskService = taskService;
        }

        public IViewComponentResult Invoke(int Id)
        {
            var result = _taskService.GetListWithCompanyAndCityAndStatus2().Where(x => x.SendCompanyId == Id || x.OrderCompanyId == Id).ToList();
            return View(result);
        }
    }
}
