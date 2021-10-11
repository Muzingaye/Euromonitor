using System;
using System.Collections.Generic;
using DataFramework.Entities;
using DataFramework.Repository;

namespace ServiceFramework.Services
{
    public class AccountDetailsServices : IService<AccountDetails>
    {
        private AccountDetailsRepository _repo;

        public AccountDetailsServices(AccountDetailsRepository repo)
        {
            this._repo = repo;
        }
        public IEnumerable<AccountDetails> GetAll()
        {
            throw new NotImplementedException();
        }

        public AccountDetails GetFirst(Func<AccountDetails, bool> filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AccountDetails> GetWhere(Func<AccountDetails, bool> filters)
        {
            return _repo.GetWhere(filters);
        }

        public bool Insert(AccountDetails instance)
        {
            return _repo.Insert(instance);
        }

        public bool InsertRange(List<AccountDetails> instance)
        {
            throw new NotImplementedException();
        }

        public bool Update(AccountDetails instance)
        {
            throw new NotImplementedException();
        }
    }
}