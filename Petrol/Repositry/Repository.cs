using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrol.Repositry
{
    public class Repository : IRepository
    {
        public void Add<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }
        public void Delete<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }
        public IEnumerable<T> Find<T>(Func<T, bool> predicate) where T : class
        {
            throw new NotImplementedException();
        }
        public IEnumerable<T> GetAll<T>() where T : class
        {
            throw new NotImplementedException();
        }
        public T GetById<T>(int id) where T : class
        {
            throw new NotImplementedException();
        }
        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
        public void Update<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }
    }
    
}

