using System;

namespace DataFramework.Interface
{
    public interface IAccount
    {
        int Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string PhoneNumber { get; set; }
        string EmailAddress { get; set; }
        DateTime CaptureDate { get; set; }
    }
}