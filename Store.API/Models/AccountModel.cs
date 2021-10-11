using System;
using System.ComponentModel.DataAnnotations;

namespace Store.API.Models
{
    public class AccountModel : ErrorBase
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        public DateTime CaptureDate { get; set; }
        public string Password { get; set; }
        public AccountDetailsModel AccountDetails { get; set; }
    }


    public class AccountDetailsModel
    {
        public int AccountId { get; set; }
        public string Password { get; set; }
        public decimal ActiveMin { get; set; }
        public DateTime LastLogInTime { get; set; }
        public DateTime CaptureDate { get; set; }
        public virtual AccountModel Account { get; set; }

        public AccountDetailsModel()
        {

        }

        public AccountDetailsModel(AccountDetailsModel _account)
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