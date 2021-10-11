using System.Collections.Generic;
using DataFramework.Entities;

namespace DataFramework.Interface
{
    public interface IReport
    {
        List<Subscription> YourSubscriptions();
    }
}