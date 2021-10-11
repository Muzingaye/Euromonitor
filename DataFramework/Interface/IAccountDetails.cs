using System;
using DataFramework.Entities;

namespace DataFramework.Interface
{
    public interface IAccountDetails
    {
        int AccountId { get; set; }
        string Password { get; set; }
        decimal ActiveMin { get; set; }
        DateTime LastLogInTime { get; set; }
        DateTime CaptureDate { get; set; }
        Account Account { get; set; }
    }
}