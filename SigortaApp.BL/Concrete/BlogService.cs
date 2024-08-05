using SigortaApp.BL.Abstract;
using SigortaApp.Entity.Concrete;
using SigortaApp.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.BL.Concrete
{
    public class BlogService : IBlogService
    {
        IBlogRepository _blogDal;

        public BlogService(IBlogRepository blogDal)
        {
            _blogDal = blogDal;
        }
        
        public Blog TGetById(int id)
        {
            return _blogDal.GetById(id);
        }

        public List<Blog> GetList()
        {
            return _blogDal.GetListAll();
        }

        public List<Blog> GetLastThreeBlog()
        {
            return _blogDal.GetListAll().Take(3).ToList();
        }

        public List<Blog> GetListWithCategory()
        {
            return _blogDal.GetListWithCategory();
        }
        public List<Blog> GetListWithCategoryByWriterBm(int Id)
        {
            return _blogDal.GetListWithCategoryByWriter(Id);
        }

        public List<Blog> GetBlogById(int id)
        {
            return _blogDal.GetListAll(x => x.Id == id);
        }

        public List<Blog> GetListByWriter(int id)
        {
            return _blogDal.GetListAll(x => x.WriterId == id);
        }

        public void TAdd(Blog t)
        {
            _blogDal.Insert(t);
        }

        public void TDelete(Blog t)
        {
            _blogDal.Delete(t);
        }

        public void TUpdate(Blog t)
        {
            _blogDal.Update(t);
        }
    }
}
