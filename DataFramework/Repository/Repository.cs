using System;
using System.Collections.Generic;
using System.Linq;
using DataFramework.Context;
using DataFramework.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataFramework.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly EuroContext _context;
        public Repository(string connString) => _context = new EuroContext(connString);
        public virtual IEnumerable<T> GetAll() => _context.Set<T>().ToList();

        public virtual IEnumerable<T> GetWhere(Func<T, bool> filters) => _context.Set<T>().Where(filters);

        public virtual T GetFirst(Func<T, bool> filters) => GetWhere(filters).FirstOrDefault();

        public virtual bool Insert(T instance)
        {
            _context.Set<T>().Add(instance);
            _context.SaveChanges();
            return true;
        }

        public virtual bool InsertRange(List<T> entities)
        {
            List<T> set = _context.Set<T>().ToList();
            entities.AddRange(set);
            _context.SaveChanges();
            return true;
        }

        public virtual bool Update(T instance)
        {
            _context.Entry(instance).State = EntityState.Modified;
            _context.SaveChanges();
            return true;
        }

        public virtual bool Delete(T key)
        {
            _context.Set<T>().Remove(key);
            _context.SaveChanges();
            return true;
        }
    }
}