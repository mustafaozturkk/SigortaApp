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
    public class CalendarService : ICalendarService
    {
        ICalendarRepository _calendarDal;

        public CalendarService(ICalendarRepository calendarDal)
        {
            _calendarDal = calendarDal;
        }

        public List<Calendar> GetList()
        {
            return _calendarDal.GetListAll();
        }

        public void TAdd(Calendar c)
        {
            _calendarDal.Insert(c);
        }

        public void TDelete(Calendar c)
        {
            _calendarDal.Delete(c);
        }

        public Calendar TGetById(int id)
        {
            return _calendarDal.GetById(id);
        }

        public void TUpdate(Calendar c)
        {
            _calendarDal.Update(c);
        }
    }
}
