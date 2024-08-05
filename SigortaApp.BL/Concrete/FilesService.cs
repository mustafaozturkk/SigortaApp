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
    public class FilesService : IFilesService
    {
        private readonly IFilesRepository _filesRepository;

        public FilesService(IFilesRepository filesRepository)
        {
            _filesRepository = filesRepository;
        }

        public List<Files> GetList()
        {
            return _filesRepository.GetAll();
        }

        public void TAdd(Files t)
        {
            _filesRepository.Insert(t);
        }

        public void TDelete(Files t)
        {
            _filesRepository.Delete(t);
        }

        public Files TGetById(int id)
        {
            return _filesRepository.GetById(id);
        }

        public void TUpdate(Files t)
        {
            _filesRepository.Update(t);
        }
    }
}
