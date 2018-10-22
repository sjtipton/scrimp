using Microsoft.EntityFrameworkCore;
using scrimp.Helpers.Timestamps;
using System;
using System.Linq;

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
        }

        public override int SaveChanges()
        {
            var added = ChangeTracker.Entries<IAuditableModel>().Where(e => e.State == EntityState.Added).ToList();

            added.ForEach(e =>
            {
                e.Property(x => x.CreatedAt).CurrentValue = DateTime.UtcNow;
                e.Property(x => x.CreatedAt).IsModified = true;
            });

            var modified = ChangeTracker.Entries<IAuditableModel>().Where(e => e.State == EntityState.Modified).ToList();

            modified.ForEach(e =>
            {
                e.Property(x => x.UpdatedAt).CurrentValue = DateTime.UtcNow;
                e.Property(x => x.UpdatedAt).IsModified = true;

                e.Property(x => x.CreatedAt).CurrentValue = e.Property(x => x.CreatedAt).OriginalValue;
                e.Property(x => x.CreatedAt).IsModified = false;
            });

            return base.SaveChanges();
        }
    }
}
