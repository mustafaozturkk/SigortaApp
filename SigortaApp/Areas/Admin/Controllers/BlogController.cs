using ClosedXML.Excel;
using SigortaApp.BL.Abstract;
using SigortaApp.BL.Concrete;
using SigortaApp.Web.Areas.Admin.Models;
using SigortaApp.DAL.Concrete;
using SigortaApp.DAL.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SigortaApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly Context _context;

        public BlogController(Context context, IBlogService blogService)
        {
            _blogService = blogService;
            _context = context;
        }

        public IActionResult ExportStaticExcelBlogList()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Blog Listesi");
                worksheet.Cell(1, 1).Value = "Blog Id";
                worksheet.Cell(1, 2).Value = "Blog Adı";

                int BlogRowCount = 2;
                foreach (var item in GetBlogList())
                {
                    worksheet.Cell(BlogRowCount, 1).Value=item.BlogId;
                    worksheet.Cell(BlogRowCount, 2).Value=item.BlogName;
                    BlogRowCount++;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Calisma1.xlsx");
                }
            }
        }
        public List<BlogModel> GetBlogList()
        {
            List<BlogModel> bm = new List<BlogModel>
            {
                new BlogModel{BlogId=1,BlogName="C# Programlamaya Giriş"},
                new BlogModel{BlogId=3,BlogName="Tesla Firmasının Araçları"},
                new BlogModel{BlogId=2,BlogName="2020 Olimpiyatları"},
            };
            return bm;
        }

        public IActionResult BlogListExcel()
        {
            return View();
        }

        public IActionResult ExportDynamicExcelBlogList()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Blog Listesi");
                worksheet.Cell(1, 1).Value = "Blog Id";
                worksheet.Cell(1, 2).Value = "Blog Adı";
                worksheet.Cell(1, 3).Value = "Blog Tarih";

                int BlogRowCount = 2;
                foreach (var item in _blogService.GetList())
                {
                    //worksheet.Cell(BlogRowCount, 1).Value = item.BlogId;
                    //worksheet.Cell(BlogRowCount, 2).Value = item.BlogName;
                    worksheet.Cell(BlogRowCount, 1).Value = item.Id;
                    worksheet.Cell(BlogRowCount, 2).Value = item.Title;
                    worksheet.Cell(BlogRowCount, 3).Value = item.CreateDate;
                    BlogRowCount++;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Calisma1.xlsx");
                }
            }
        }
        public List<BlogModel2> BlogTitleList()
        {
            List<BlogModel2> bm = new List<BlogModel2>();
            using (var c = _context)
            {
                bm = c.Blogs.Select(x => new BlogModel2
                {
                    BlogId=x.Id,
                    BlogName=x.Title
                }).ToList();
            }
            return bm;
        }
        public IActionResult BlogTitleListExcel()
        {
            return View();
        }
    }
}
