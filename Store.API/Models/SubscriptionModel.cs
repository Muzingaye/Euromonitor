using System;

namespace Store.API.Models
{
    public class SubscriptionModel : ErrorBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime CaptureDate { get; set; }
    }
}