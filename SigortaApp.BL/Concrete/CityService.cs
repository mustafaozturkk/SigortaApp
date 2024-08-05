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
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public List<City> GetList()
        {
            return _cityRepository.GetAll();
        }

        public void TAdd(City t)
        {
            throw new NotImplementedException();
        }

        public void TDelete(City t)
        {
            throw new NotImplementedException();
        }

        public City TGetById(int id)
        {
            return _cityRepository.GetById(id);
        }

        public void TUpdate(City t)
        {
            throw new NotImplementedException();
        }
    }
}
