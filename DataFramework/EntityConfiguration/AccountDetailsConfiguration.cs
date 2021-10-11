using DataFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataFramework.EntityConfiguration
{
    public class AccountDetailsConfiguration
    {
        public AccountDetailsConfiguration(EntityTypeBuilder<AccountDetails> entity)
        {
            entity.ToTable("AccountDetails", "Admin");
            entity.HasKey(u => u.AccountId);
            entity.Property(u => u.Password).HasMaxLength(100).IsRequired();
            entity.Property(u => u.ActiveMin).HasMaxLength(100).IsRequired();
            entity.Property(u => u.LastLogInTime).IsRequired();
            entity.Property(u => u.CaptureDate).IsRequired();
            entity.Property(c => c.AccountId).HasColumnName("Id").IsRequired();
        }
    }
}