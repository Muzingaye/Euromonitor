using DataFramework.Entities;

namespace DataFramework.Repository
{
    public class AccountDetailsRepository : Repository<AccountDetails>
    {
        public AccountDetailsRepository(string connString) : base(connString)
        {
        }
    }
}