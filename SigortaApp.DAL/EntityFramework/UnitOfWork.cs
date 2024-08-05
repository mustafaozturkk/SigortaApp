using SigortaApp.Entity.Concrete;
using SigortaApp.DAL.Abstract;
using SigortaApp.DAL.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.DAL.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context _context;

        public UnitOfWork(Context context)
        {
            _context = context;
            aboutDal = new EFAboutRepository(_context);
            adminDal = new EFAdminRepository(_context);
            blogDal = new EFBlogRepository(_context);
            brandDal = new EFBrandRepository(_context);
            categoryDal = new EFCategoryRepository(_context);
            commentDal = new EFCommentRepository(_context);
            contactDal = new EFContactRepository(_context);
            devicesDal = new EFDevicesRepository(_context);
            devicesReleaseDal = new EFDevicesReleaseRepository(_context);
            messageDal = new EFMessageRepository(_context);
            message2Dal = new EFMessage2Repository(_context);
            newsLetterDal = new EFNewsLetterRepository(_context);
            notificationDal = new EFNotificationRepository(_context);
            typesDal = new EFTypesRepository(_context);
            unitDal = new EFUnitRepository(_context);
            userDal = new EFUserRepository(_context);
            writerDal = new EFWriterRepository(_context);
            taskDal = new EFTaskRepository(_context);
            taskHistoryDal = new EFTaskHistoryRepository(_context);
            paymentTaskDal = new EFPaymentTaskRepository(_context);
        }

        public IAboutRepository aboutDal {get; private set;}

        public IAdminRepository adminDal {get; private set;}

        public IBlogRepository blogDal {get; private set;}

        public IBrandRepository brandDal {get; private set;}

        public ICategoryRepository categoryDal {get; private set;}

        public ICommentRepository commentDal {get; private set;}

        public IContactRepository contactDal {get; private set;}

        public IDevicesRepository devicesDal {get; private set;}

        public IDevicesReleaseRepository devicesReleaseDal {get; private set;}

        public IMessageRepository messageDal {get; private set;}

        public IMessage2Repository message2Dal {get; private set;}

        public INewsLetterRepository newsLetterDal {get; private set;}

        public INotificationRepository notificationDal {get; private set;}

        public ITypesRepository typesDal {get; private set;}

        public IUnitRepository unitDal {get; private set;}

        public IUserRepository userDal {get; private set;}

        public IWriterRepository writerDal {get; private set;}
        public ITaskRepository taskDal {get; private set;}
        public ITaskHistoryRepository taskHistoryDal {get; private set;}
        public IPaymentTaskRepository paymentTaskDal {get; private set;}

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
