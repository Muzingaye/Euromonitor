using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceFramework
{
    public interface IService<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetFirst(Func<TEntity, bool> filters);
        IEnumerable<TEntity> GetWhere(Func<TEntity, bool> filters);
        bool Insert(TEntity instance);
        bool InsertRange(List<TEntity> instance);
        bool Update(TEntity instance);
    }
}
