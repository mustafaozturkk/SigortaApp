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
    public class StatusService : IStatusService
    {
        IStatusRepository _statusRepository;

        public StatusService(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public List<Status> GetList()
        {
            throw new NotImplementedException();
        }

        public void TAdd(Status t)
        {
            throw new NotImplementedException();
        }

        public void TDelete(Status t)
        {
            throw new NotImplementedException();
        }

        public Status TGetById(int id)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(Status t)
        {
            throw new NotImplementedException();
        }
    }
}
