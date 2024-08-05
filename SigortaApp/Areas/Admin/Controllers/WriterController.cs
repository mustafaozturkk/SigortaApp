using SigortaApp.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace SigortaApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WriterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult WriterList()
        {
            var jsonWriter = JsonConvert.SerializeObject(writers);
            return Json(jsonWriter);
        }

        public IActionResult GetWriterById(int writerid)
        {
            var findWriter = writers.FirstOrDefault(x => x.Id == writerid);
            var jsonwriters = JsonConvert.SerializeObject(findWriter);
            return Json(jsonwriters);
        }

        public IActionResult DeleteWriter(int writerid)
        {
            var writer = writers.FirstOrDefault(x => x.Id == writerid);
            writers.Remove(writer);
            return Json(writer);
        }

        [HttpPost]
        public IActionResult AddWriter(WriterClass w)
        {
            writers.Add(w);
            var jsonWriter = JsonConvert.SerializeObject(writers);
            return Json(jsonWriter);
        }

        public IActionResult UpdateWriter(WriterClass w)
        {
            var writer = writers.FirstOrDefault(x => x.Id == w.Id);
            writer.Name = w.Name;
            var jsonWriter = JsonConvert.SerializeObject(writer);
            return Json(jsonWriter);
        }

        public static List<WriterClass> writers = new List<WriterClass>
        {
            new WriterClass
            {
                Id=1,
                Name="Ayşe"
            },
            new WriterClass
            {
                Id=2,
                Name="Ahmet"
            },
            new WriterClass
            {
                Id=3,
                Name="Buse"
            },
        };
    }
}
