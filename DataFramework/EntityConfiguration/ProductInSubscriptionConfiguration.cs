using DataFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataFramework.EntityConfiguration
{
    public class ProductInSubscriptionConfiguration
    {
        public ProductInSubscriptionConfiguration(EntityTypeBuilder<ProductInSubscription> entity)
        {
            entity.ToTable("ProductOrder", "Admin");
            entity.HasKey(u => new { u.ProductId, u.OrderId, u.AccountId });
            entity.Property(u => u.CaptureDate).IsRequired();
        }
    }
}