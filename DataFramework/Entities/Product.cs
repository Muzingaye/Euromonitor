using System;
using System.Collections.Generic;
using DataFramework.Interface;

namespace DataFramework.Entities
{
    public class Product : IProduct
    {
        public Product()
        {
            Subscriptions = new HashSet<Subscription>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageLocation { get; set; }
        public DateTime CaptureDate { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; }
    }
}