using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
  public void Configure(EntityTypeBuilder<Order> builder)
  {
    builder.ToTable("Orders");

    builder.HasKey(o => o.Id);

    builder.Property(o => o.Id)
                .HasConversion(
                    orderId => orderId.Value, 
                    value => new OrderId(value))
                .IsRequired()
                .HasColumnName("Id");

    builder.Property(o => o.CustomerId)
        .IsRequired()
        .HasColumnName("CustomerId");

    builder.Property(o => o.CreatedAt)
        .IsRequired()
        .HasColumnName("CreatedAt");

    builder.Property(o => o.UpdatedAt)
        .IsRequired()
        .HasColumnName("UpdatedAt");

    builder.Property(o => o.Status)
        .IsRequired()
        .HasConversion<string>()
        .HasColumnName("Status");

    builder.Property(o => o.PaymentMethod)
        .IsRequired()
        .HasConversion<string>()
        .HasColumnName("PaymentMethod");

    builder.Property(o => o.TotalAmount)
        .HasColumnType("decimal(18,2)") 
        .IsRequired()
        .HasColumnName("TotalAmount");


    builder.Property(o => o.Comments)
   .HasMaxLength(1000) 
   .HasColumnName("Comments");

     builder
        .HasMany(o => o.Items)
        .WithOne()
        .HasForeignKey(i => i.OrderId)
        .OnDelete(DeleteBehavior.Cascade);
  }
}
