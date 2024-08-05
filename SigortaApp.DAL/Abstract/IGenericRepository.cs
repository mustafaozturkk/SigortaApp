using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.DAL.Abstract
{
    public interface IGenericRepository<T> where T : class
    {
        void Insert (T item);
        void Update (T item);
        void Delete (T item);
        List<T> GetAll ();
        T GetById (int id);
        List<T> GetListAll ();
        List<T> GetListAll(Expression<Func<T,bool>> filter);
    }
}
