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
    }
}
