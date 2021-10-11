using System.Collections.Generic;
using DataFramework.Entities;
using DataFramework.Interface;

namespace DataFramework.Repository
{
    public class SubscriptionRepository : Repository<Subscription>, IReport
    {
        public SubscriptionRepository(string connString) : base(connString)
        {
        }

        public List<Subscription> YourSubscriptions()
        {
            throw new System.NotImplementedException();
        }
    }
}