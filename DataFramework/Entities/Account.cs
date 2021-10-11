using System;
using DataFramework.Interface;

namespace DataFramework.Entities
{
    public class Account : IAccount
    {
        public Account(Account account)
        {
            Id = account.Id;
            FirstName = account.FirstName;
            LastName = account.LastName;
            PhoneNumber = account.PhoneNumber;
            EmailAddress = account.EmailAddress;
            CaptureDate = account.CaptureDate;
        }

        public Account()
        {
            
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public DateTime CaptureDate { get; set; }
        public AccountDetails AccountDetails { get; set; }
    }
}