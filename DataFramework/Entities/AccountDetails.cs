using System;
using DataFramework.Interface;

namespace DataFramework.Entities
{
    public class AccountDetails : IAccountDetails
    {
        public int AccountId { get; set; }
        public string Password { get; set; }
        public decimal ActiveMin { get; set; }
        public DateTime LastLogInTime { get; set; }
        public DateTime CaptureDate { get; set; }
        public Account Account { get; set; }

        public AccountDetails()
        {
            
        }
        public AccountDetails(AccountDetails _account)
        {
            AccountId = _account.AccountId;
            Password = _account.Password;
            ActiveMin = _account.ActiveMin;
            LastLogInTime = _account.LastLogInTime;
            CaptureDate = _account.CaptureDate;
            Account = _account.Account;
        }
    }
}