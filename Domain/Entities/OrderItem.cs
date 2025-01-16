using Domain.Primitives;

namespace Domain.Entities
{
  public sealed class OrderItem : AggregateRoot
  {
    public int Id { get; private set; }

    public int ProductId { get; private set; }

    public int Quantity { get; private set; }

    public OrderId OrderId { get; private set; }

    public decimal UnitPrice { get; private set; }

    private OrderItem() { }

    public OrderItem(int productId, int quantity, decimal unitPrice, OrderId orderId)
    {
      if (productId <= 0)
      {
        throw new ArgumentException("ProductId deve ser válido.", nameof(productId));
      }

      if (quantity <= 0)
      {
        throw new ArgumentException("Quantity deve ser maior que zero.", nameof(quantity));
      }

      ProductId = productId;
      Quantity = quantity;
      UnitPrice = unitPrice;
      OrderId = orderId;
    }
  }
}

