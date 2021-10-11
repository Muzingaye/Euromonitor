using System;

namespace Store.API.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageLocation { get; set; }
        public DateTime CaptureDate { get; set; }
    }
}