using System;
using System.Linq;
using DataFramework.Context;
using DataFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataFramework.Repository
{
    public class AccountRepository : Repository<Account>
    {
        private EuroContext context;
        public AccountRepository(string connString) : base(connString)
        {
            this.context = new EuroContext(connString);
        }
        public override Account GetFirst(Func<Account, bool> filters)
        {
           return context.Account.Include(ad => ad.AccountDetails).Where(filters).FirstOrDefault();
        }
    }
}