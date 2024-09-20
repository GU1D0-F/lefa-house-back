using Core.Expenses;
using Core.Payments;
using Core.Transactions;
using Microsoft.EntityFrameworkCore;

namespace Core.Data
{
    public class CoreDbContext : DbContext
    {
        public CoreDbContext()
        {
        }

        public CoreDbContext(DbContextOptions options) : base(options)
        {
        }


        public virtual DbSet<Payment> Payments => Set<Payment>();
        public virtual DbSet<Expense> Expenses => Set<Expense>();
        public virtual DbSet<Transaction> Transactions => Set<Transaction>();
    }
}
