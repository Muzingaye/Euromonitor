using DataFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataFramework.EntityConfiguration
{
    public class AccountConfiguration
    {
        public AccountConfiguration(EntityTypeBuilder<Account> entity)
        {
            entity.ToTable("Account", "Admin");
            entity.HasKey(u => u.Id);
            entity.Property(u => u.FirstName).HasMaxLength(30).IsRequired();
            entity.Property(u => u.LastName).HasMaxLength(30).IsRequired();
            entity.Property(u => u.PhoneNumber).HasMaxLength(30);
            entity.Property(u => u.EmailAddress).HasMaxLength(30);
            entity.Property(u => u.CaptureDate).IsRequired();

        }
    }
}