using DataFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataFramework.EntityConfiguration
{
    public class SubscriptionConfiguration
    {
        public SubscriptionConfiguration(EntityTypeBuilder<Subscription> entity)
        {
            entity.ToTable("Order", "Admin");
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Name).HasMaxLength(30).IsRequired();
            entity.Property(u => u.CaptureDate).IsRequired();
        }
    }
}