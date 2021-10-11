using System;

namespace DataFramework.Entities
{
    public class ProductInSubscription 
    {
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int AccountId { get; set; }
        public DateTime CaptureDate { get; set; }
        public virtual Product Product { get; set; }
        public virtual Subscription Subscription { get; set; }
    }
}