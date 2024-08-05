using SigortaApp.Entity.Concrete;
using SigortaApp.Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = SigortaApp.Entity.Concrete.Task;

namespace SigortaApp.BL.Abstract
{
    public interface ITaskService : IGenericService<Task>
    {
        //List<TaskDto> GetListWithCompanyAndCityAndStatus();
        List<TaskDto> GetListWithCompanyAndCityAndStatus2();
        List<TaskDto> GetListWithCompanyCityAndStatus();
        TaskDto GetTaskAndCityByTaskId(int Id);
        TaskDto GetTaskWithCompanyAndCityAndStatusByTaskId(int Id);
    }
}
