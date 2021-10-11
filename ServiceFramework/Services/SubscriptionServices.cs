using System;
using System.Collections.Generic;
using DataFramework.Entities;
using DataFramework.Repository;

namespace ServiceFramework.Services
{
    public class SubscriptionServices : IService<Subscription>
    {

        private SubscriptionRepository _subscriptionRepository;

        public SubscriptionServices(SubscriptionRepository subscription)
        {
            this._subscriptionRepository = subscription;
        }
        public IEnumerable<Subscription> GetAll()
        {
            return _subscriptionRepository.GetAll();
        }

        public Subscription GetFirst(Func<Subscription, bool> filters)
        {
            return _subscriptionRepository.GetFirst(filters);
        }

        public IEnumerable<Subscription> GetWhere(Func<Subscription, bool> filters)
        {
            return _subscriptionRepository.GetWhere(filters);
        }

        public bool Insert(Subscription instance)
        {
            return _subscriptionRepository.Insert(instance);
        }

        public bool InsertRange(List<Subscription> instance)
        {
            return _subscriptionRepository.InsertRange(instance);
        }

        public bool Update(Subscription instance)
        {
            return _subscriptionRepository.Update(instance);
        }

        public virtual bool Delete(Subscription key)
        {
            return _subscriptionRepository.Delete(key);
        }
    }
}