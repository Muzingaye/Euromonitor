using DataFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataFramework.EntityConfiguration
{
    public class ProductConfiguration
    {
        public ProductConfiguration(EntityTypeBuilder<Product> entity)
        {
            entity.ToTable("Product", "Admin");
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Name).HasMaxLength(30).IsRequired();
            entity.Property(u => u.Description).HasMaxLength(120).IsRequired();
            entity.Property(u => u.Price).HasColumnType("decimal(10, 2)");
            entity.Property(u => u.ImageLocation).HasMaxLength(30);
            entity.Property(u => u.CaptureDate).IsRequired();
        }
    }
}