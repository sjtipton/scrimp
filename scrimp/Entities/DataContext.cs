using Microsoft.EntityFrameworkCore;

namespace scrimp.Entities
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<TransactionAccount> TransactionAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Error> Errors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Transaction>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Transaction>()
                .Property(p => p.ClosingBalance)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Account>()
                .Property(p => p.CurrentBalance)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<TransactionAccount>()
                .Property(p => p.CurrentBalance)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<TransactionAccount>()
                .Property(p => p.StartingBalance)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Error>()
                .OwnsOne(p => p.InnerException);
        }
    }
}
