using System;
using System.Collections.Generic;

namespace DataFramework.Interface
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetWhere(Func<T, bool> filters);
        T GetFirst(Func<T, bool> filters);
        bool Insert(T instance);
        bool InsertRange(List<T> entities);
        bool Update(T instance);
        bool Delete(T key);
    }
}