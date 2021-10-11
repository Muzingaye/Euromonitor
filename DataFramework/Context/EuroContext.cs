using DataFramework.Entities;
using DataFramework.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace DataFramework.Context
{
    public class EuroContext : DbContext
    {
        public readonly string ConnString;

        public DbSet<Account> Account { get; set; }
        public DbSet<AccountDetails> AccountDetails { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Subscription> Subscription { get; set; }
        public DbSet<ProductInSubscription> ProductInSubscription { get; set; }
        public EuroContext(string connString) => this.ConnString = connString;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new AccountConfiguration(modelBuilder.Entity<Account>());
            new AccountDetailsConfiguration(modelBuilder.Entity<AccountDetails>());
            new ProductConfiguration(modelBuilder.Entity<Product>());
            new SubscriptionConfiguration(modelBuilder.Entity<Subscription>());
            new ProductInSubscriptionConfiguration(modelBuilder.Entity<ProductInSubscription>());
        }
        private readonly string conn = "Data Source=.\\SQLExpress;Initial Catalog=BetDb;Integrated Security=True";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(conn);
        }
    }
}