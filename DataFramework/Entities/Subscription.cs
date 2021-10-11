using System;
using System.Collections.Generic;
using DataFramework.Interface;

namespace DataFramework.Entities
{
    public sealed class Subscription : ISubscription
    {
        public Subscription()
        {
            Products = new HashSet<Product>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CaptureDate { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}