using System;
using System.Collections.Generic;
using DataFramework.Entities;
using DataFramework.Repository;

namespace ServiceFramework.Services
{
    public class ProductInSubscriptionService : IService<ProductInSubscription>
    {
        private ProductInSubscriptionRepository _repo;

        public ProductInSubscriptionService(ProductInSubscriptionRepository subscription)
        {
            this._repo = subscription;
        }

        public IEnumerable<ProductInSubscription> GetAll()
        {
            throw new NotImplementedException();
        }

        public ProductInSubscription GetFirst(Func<ProductInSubscription, bool> filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductInSubscription> GetWhere(Func<ProductInSubscription, bool> filters)
        {
            return _repo.GetWhere(filters);
        }

        public bool Insert(ProductInSubscription instance)
        {
            return _repo.Insert(instance);
        }

        public bool InsertRange(List<ProductInSubscription> instance)
        {
            return _repo.InsertRange(instance);
        }

        public bool Update(ProductInSubscription instance)
        {
            return _repo.Update(instance);
        }

        public virtual bool Delete(ProductInSubscription key)
        {
            return _repo.Delete(key);
        }
    }
}