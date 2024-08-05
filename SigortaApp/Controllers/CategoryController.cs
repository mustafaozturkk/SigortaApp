using SigortaApp.BL.Abstract;
using SigortaApp.BL.Concrete;
using SigortaApp.DAL.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace SigortaApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var values = _categoryService.GetList();
            return View(values);
        }
    }
}
