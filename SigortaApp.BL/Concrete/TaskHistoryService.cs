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
    public class TaskHistoryService : ITaskHistoryService
    {
        ITaskHistoryRepository _taskHistoryRepository;

        public TaskHistoryService(ITaskHistoryRepository taskHistoryRepository)
        {
            _taskHistoryRepository = taskHistoryRepository;
        }

        public List<TaskHistory> GetList()
        {
            return _taskHistoryRepository.GetListAll();
        }

        public void TAdd(TaskHistory t)
        {
            _taskHistoryRepository.Insert(t);
        }

        public void TDelete(TaskHistory t)
        {
            throw new NotImplementedException();
        }

        public TaskHistory TGetById(int id)
        {
            return _taskHistoryRepository.GetById(id);
        }

        public void TUpdate(TaskHistory t)
        {
            throw new NotImplementedException();
        }
    }
}
