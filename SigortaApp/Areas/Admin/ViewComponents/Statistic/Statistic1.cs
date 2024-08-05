using SigortaApp.BL.Abstract;
using SigortaApp.BL.Concrete;
using SigortaApp.DAL.Concrete;
using SigortaApp.DAL.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Xml.Linq;

namespace SigortaApp.Web.Areas.Admin.ViewComponents.Statistic
{
    public class Statistic1 : ViewComponent
    {
        private readonly IBlogService _blogService;
        private readonly Context _context;

        public Statistic1(Context context, IBlogService blogService)
        {
            _blogService = blogService;
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.BlogSayisi = _blogService.GetList().Count();
            ViewBag.MesajSayisi = _context.Contacts.Count();
            ViewBag.YorumSayisi = _context.Comments.Count();

            string api = "997b5b55b05f3eb92952b60796b9bec9";
            string connection = "https://api.openweathermap.org/data/2.5/weather?q=Ankara&mode=xml&lang=tr&units=metric&appid=" + api;
            XDocument document = XDocument.Load(connection);
            ViewBag.v4 = document.Descendants("temperature").ElementAt(0).Attribute("value").Value;
            return View();
        }
    }
}
