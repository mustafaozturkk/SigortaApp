using SigortaApp.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.DAL.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IAboutRepository aboutDal { get; }
        IAdminRepository adminDal { get; }
        IBlogRepository blogDal { get; }
        IBrandRepository brandDal { get; }
        ICategoryRepository categoryDal { get; }
        ICommentRepository commentDal { get; }
        IContactRepository contactDal { get; }
        IDevicesRepository devicesDal { get; }
        IDevicesReleaseRepository devicesReleaseDal { get; }
        IMessageRepository messageDal { get; }
        IMessage2Repository message2Dal { get; }
        INewsLetterRepository newsLetterDal { get; }
        INotificationRepository notificationDal { get; }
        ITypesRepository typesDal { get; }
        IUnitRepository unitDal { get; }
        IUserRepository userDal { get; }
        IWriterRepository writerDal { get; }
        ITaskRepository taskDal { get; }
        ITaskHistoryRepository taskHistoryDal { get; }
        IPaymentTaskRepository paymentTaskDal { get; }
        int Complete();
    }
}
