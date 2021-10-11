using System;

namespace DataFramework.Interface
{
    public interface ISubscription
    {
        int Id { get; set; }
        string Name { get; set; }
        DateTime CaptureDate { get; set; }

    }
}