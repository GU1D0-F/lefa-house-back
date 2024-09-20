using Core.Expenses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.ToTable("Expenses");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Amount).HasColumnType("decimal(10, 2)").IsRequired();
            builder.Property(e => e.Description).IsRequired();
            builder.Property(e => e.Category).IsRequired();
            builder.Property(e => e.Date).IsRequired();
            builder.Property(e => e.UserName).IsRequired();
        }
    }
}
