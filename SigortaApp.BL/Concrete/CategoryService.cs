using SigortaApp.BL.Abstract;
using SigortaApp.Entity.Concrete;
using SigortaApp.DAL.Abstract;
using SigortaApp.DAL.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.BL.Concrete
{
    public class CategoryService : ICategoryService
    {
        ICategoryRepository _categoryDal;

        public CategoryService(ICategoryRepository categoryDal)
        {
            _categoryDal = categoryDal;
        }
        public Category TGetById(int id)
        {
            return _categoryDal.GetById(id);
        }
        public List<Category> GetList()
        {
            return _categoryDal.GetListAll();
        }
        public void TAdd(Category t)
        {
            _categoryDal.Insert(t);
        }
        public void TDelete(Category t)
        {
            _categoryDal.Delete(t);
        }
        public void TUpdate(Category t)
        {
            _categoryDal.Update(t);
        }
    }
}
