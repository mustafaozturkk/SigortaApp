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
    public class BrandService : IBrandService
    {
        IBrandRepository _brandDal;

        public BrandService(IBrandRepository brandDal)
        {
            _brandDal = brandDal;
        }

        public List<Brand> GetList()
        {
            return _brandDal.GetListAll();
        }

        public void TAdd(Brand t)
        {
            _brandDal.Insert(t);
        }

        public void TDelete(Brand t)
        {
            _brandDal.Delete(t);
        }

        public Brand TGetById(int id)
        {
            return _brandDal.GetById(id);
        }

        public void TUpdate(Brand t)
        {
            _brandDal.Update(t);
        }
    }
}
