using Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class ApplicationDbContext(DbContextOptions<CoreDbContext> options) : CoreDbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
