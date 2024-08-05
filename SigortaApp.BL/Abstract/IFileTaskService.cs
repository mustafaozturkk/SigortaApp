using SigortaApp.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.BL.Abstract
{
    public interface IFileTaskService : IGenericService<FileTask>
    {
        List<FileTask> GetFileTaskByTaskId(int taskId);
    }
}
