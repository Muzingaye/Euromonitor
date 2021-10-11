using DataFramework.Entities;

namespace DataFramework.Repository
{
    public class ProductInSubscriptionRepository : Repository<ProductInSubscription>
    {
        public ProductInSubscriptionRepository(string connString) : base(connString)
        {
        }
    }
}