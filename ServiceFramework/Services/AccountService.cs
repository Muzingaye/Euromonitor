using System;
using System.Collections.Generic;
using DataFramework.Entities;
using DataFramework.Repository;

namespace ServiceFramework.Services
{
    public class AccountService : IService<Account>
    {
        private AccountRepository accountRepository;

        public AccountService(AccountRepository account)
        {
            this.accountRepository = account;
        }
        public AccountService()
        {
            
        }
        public IEnumerable<Account> GetAll()
        {
            return accountRepository.GetAll();
        }

        public virtual Account GetFirst(Func<Account, bool> filters)
        {
            return accountRepository.GetFirst(filters);
        }

        public virtual IEnumerable<Account> GetWhere(Func<Account, bool> filters)
        {
            return accountRepository.GetWhere(filters);
        }

        public virtual bool Insert(Account instance)
        {
            if (!Validation.ValidateString(instance.FirstName))
            {
                throw new Exception($"Invalid FirstName {instance.FirstName}, Only characters are allowed");
            }

            if (!Validation.ValidateString(instance.LastName))
            {
                throw new Exception($"Invalid LastName {instance.LastName}, Only characters are allowed");
            }
            return accountRepository.Insert(instance);
        }

        public virtual bool InsertRange(List<Account> instance)
        {
            foreach (var item in instance)
            {
                if (!Validation.ValidateString(item.FirstName) || String.IsNullOrEmpty(item.EmailAddress))
                {
                    throw new Exception($"Invalid FirstName {item.FirstName}, Only characters are allowed");
                }
                if (!Validation.ValidateString(item.LastName) || String.IsNullOrEmpty(item.EmailAddress))
                {
                    throw new Exception($"Invalid LastName {item.LastName}, Only characters are allowed");
                }
            }

            return accountRepository.InsertRange(instance);
        }

        public virtual bool Update(Account instance)
        {
            if (!Validation.ValidateString(instance.FirstName))
            {
                throw new Exception($"Invalid FirstName {instance.FirstName}, Only characters are allowed");
            }
            if (!Validation.ValidateString(instance.LastName))
            {
                throw new Exception($"Invalid LastName {instance.LastName}, Only characters are allowed");
            }

            return accountRepository.Update(instance);
        }
    }
}