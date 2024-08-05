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
    public class FileTaskService : IFileTaskService
    {
        private readonly IFileTaskRepository _fileTaskRepository;

        public FileTaskService(IFileTaskRepository fileTaskRepository)
        {
            _fileTaskRepository = fileTaskRepository;
        }

        public List<FileTask> GetFileTaskByTaskId(int taskId)
        {
            return _fileTaskRepository.GetListAll().Where(w => w.TaskId == taskId).ToList();
        }

        public List<FileTask> GetList()
        {
            return _fileTaskRepository.GetListAll();
        }

        public void TAdd(FileTask t)
        {
            _fileTaskRepository.Insert(t);
        }

        public void TDelete(FileTask t)
        {
            _fileTaskRepository.Delete(t);
        }

        public FileTask TGetById(int id)
        {
            return _fileTaskRepository.GetById(id);
        }

        public void TUpdate(FileTask t)
        {
            _fileTaskRepository.Update(t);
        }
    }
}
