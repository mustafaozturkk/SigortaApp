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
    public class TypesService : ITypesService
    {
        ITypesRepository _typesDal;

        public TypesService(ITypesRepository typesDal)
        {
            _typesDal = typesDal;
        }

        public List<Types> GetList()
        {
            return _typesDal.GetListAll();
        }

        public List<Types> GetListWithBrand()
        {
            return _typesDal.GetListWithBrand();
        }

        public void TAdd(Types t)
        {
            _typesDal.Insert(t);
        }

        public void TDelete(Types t)
        {
            _typesDal.Delete(t);
        }

        public Types TGetById(int id)
        {
            return _typesDal.GetById(id);
        }

        public void TUpdate(Types t)
        {
            _typesDal.Update(t);
        }
    }
}
