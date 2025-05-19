using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrol.Repositry
{
    public interface IRepository<T> where T : class
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        T GetById<T>(int id) where T : class;
        IEnumerable<T> GetAll<T>() where T : class;
        IEnumerable<T> Find<T>(Func<T, bool> predicate) where T : class;
        void SaveChanges();
    }
}
