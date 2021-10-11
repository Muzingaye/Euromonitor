using System;

namespace DataFramework.Interface
{
    public interface IProduct
    {
        int Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        decimal Price { get; set; }
        string ImageLocation { get; set; }
        DateTime CaptureDate { get; set; }
    }
}