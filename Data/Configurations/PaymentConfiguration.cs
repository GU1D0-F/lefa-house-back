using Core.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Amount).HasColumnType("decimal(10, 2)").IsRequired();
            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.Category).IsRequired();
            builder.Property(p => p.Date).IsRequired();
            builder.Property(p => p.UserName).IsRequired();
        }
    }
}
