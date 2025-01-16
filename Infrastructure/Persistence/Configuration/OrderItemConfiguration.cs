using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
  public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
  {
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
      builder.ToTable("OrderItems");

      builder.HasKey(oi => oi.Id);

      builder.Property(oi => oi.ProductId)
          .IsRequired()
          .HasColumnName("ProductId");

      builder.Property(oi => oi.Quantity)
          .IsRequired()
          .HasColumnName("Quantity");
    
      builder.Property(oi => oi.UnitPrice)
          .HasColumnType("decimal(18,2)")
          .IsRequired()
          .HasColumnName("UnitPrice");

      builder.Property(oi => oi.OrderId)
                .HasConversion(
                    orderId => orderId.Value, 
                    value => new OrderId(value))
                .IsRequired()
                .HasColumnName("OrderId");

      builder.HasOne<Order>()
          .WithMany(o => o.Items)
          .HasForeignKey(oi => oi.OrderId)
          .OnDelete(DeleteBehavior.Cascade);
    }
  }
}
