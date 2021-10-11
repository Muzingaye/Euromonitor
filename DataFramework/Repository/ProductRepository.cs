using DataFramework.Entities;

namespace DataFramework.Repository
{
    public class ProductRepository : Repository<Product>
    {
        public ProductRepository(string connString) : base(connString)
        {
        }
    }
}