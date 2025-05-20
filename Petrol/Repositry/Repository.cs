using Microsoft.EntityFrameworkCore;
using Petrol.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Petrol.Repositry
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository()
        {
            _context = new AppDbContext();
            _dbSet = _context.Set<T>();
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
            return _context.Set<T>().AsNoTracking().Where(predicate).ToList();
        }
        public int GetTheLastId<T>() where T : class
        {
            var list = _context.Set<T>().ToList();
            var last = list.LastOrDefault();
            var type = last.GetType();
            var prop = type.GetProperty("Id");
            var val=(prop.GetValue(last) as int?)+1  ?? 1;
            return val;
        }
        public IEnumerable<T> GetAll<T>() where T : class
        {
            return _context.Set<T>().ToList();
        }
        public virtual IQueryable<T> GetAllWithInclude(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }
        public virtual IQueryable<T> GetAllWithNestedInclude(
            Func<IQueryable<T>, IQueryable<T>> includeFunc,
            Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
                query = query.Where(filter);

            if (includeFunc != null)
                query = includeFunc(query);

            return query;
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
        public void Attach<T>(T entity) where T : class
        {
            _context.Set<T>().Attach(entity);
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public  IEnumerable<T> Search(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
                return _context.Set<T>().ToList();

            var properties = typeof(T).GetProperties()
                .Where(p => p.CanRead && p.GetMethod.IsPublic);

            var list = _context.Set<T>()
                .AsEnumerable() // Bring data into memory to use reflection (note: may be costly!)
                .Where(entity =>
                    properties.Any(prop =>
                    {
                        var value = prop.GetValue(entity);
                        return value != null && value.ToString().Contains(term);
                    }))
                .ToList();

            return list;
        }
    }
    
}

