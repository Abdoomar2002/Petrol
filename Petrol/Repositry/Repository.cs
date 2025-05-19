using Microsoft.EntityFrameworkCore;
using Petrol.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrol.Repositry
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;

        public Repository()
        {
            _context = new AppDbContext();
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Set<T>().Remove(entity);
        }

        public IEnumerable<T> Find<T>(Func<T, bool> predicate) where T : class
        {
            return _context.Set<T>().Where(predicate).ToList();
        }

        public IEnumerable<T> GetAll<T>() where T : class
        {
            return _context.Set<T>().ToList();
        }

        public T GetById<T>(int id) where T : class
        {
            // Assumes entity has an "Id" property of type int
            return _context.Set<T>().Find(id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
    
}

