using Core.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Action).IsRequired();
            builder.Property(t => t.Amount).HasColumnType("decimal(10, 2)").IsRequired();
            builder.Property(t => t.Description).IsRequired();
            builder.Property(t => t.Category).IsRequired();
            builder.Property(t => t.Date).IsRequired();
        }
    }
}
