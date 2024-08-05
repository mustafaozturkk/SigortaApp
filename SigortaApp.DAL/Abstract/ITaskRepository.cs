using SigortaApp.Entity.Concrete;
using SigortaApp.Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = SigortaApp.Entity.Concrete.Task;

namespace SigortaApp.DAL.Abstract
{
    public interface ITaskRepository : IGenericRepository<Task>
    {
        //List<TaskDto> GetListWithCompanyAndCityAndStatus();
    }
}
