using Domain.Enums;
using Domain.Primitives;

namespace Domain.Entities
{
  public sealed class Order : AggregateRoot
  {
    private readonly List<OrderItem> _items = new();

    public Order(
        OrderId id,
        Guid customerId,
        DateTime createdAt,
        OrderStatus status,
        PaymentMethod paymentMethod,
        decimal totalAmount,
        List<OrderItem> items,
        string? comments = null)
    {
      Id = id;
      CustomerId = customerId;
      CreatedAt = createdAt;
      UpdatedAt = createdAt;
      Status = status;
      PaymentMethod = paymentMethod;
      TotalAmount = totalAmount;
      Comments = comments;

      _items = items ?? new List<OrderItem>();
    }

    // Properties
    public OrderId Id { get; private set; }

    /// <summary>
    /// Identifies which customer placed the order.
    /// </summary>
    public Guid CustomerId { get; private set; }

    /// <summary>
    /// Date and time when the order was created.
    /// </summary>
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    /// <summary>
    /// Date and time when the order was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; private set; }

    /// <summary>
    /// Current status of the order (e.g., Open, Processing, Completed, Canceled).
    /// </summary>
    public OrderStatus Status { get; private set; } = OrderStatus.Open;

    /// <summary>
    /// Indicates the chosen payment method (CreditCard, DebitCard, Pix, etc.).
    /// </summary>
    public PaymentMethod PaymentMethod { get; private set; }

    /// <summary>
    /// The total amount for this order, considering all items, taxes, shipping, etc.
    /// </summary>
    public decimal TotalAmount { get; private set; }

    /// <summary>
    /// Optional notes or comments, e.g., special instructions for delivery.
    /// </summary>
    public string? Comments { get; private set; }

    /// <summary>
    /// The items contained in the order.
    /// </summary>
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    // Parameterless constructor for ORM use only
    private Order() {}

    // Example method to update the order status
    public void UpdateStatus(OrderStatus newStatus)
    {
      Status = newStatus;
      UpdatedAt = DateTime.UtcNow;
    }

    // Example method to add or remove items
    public void AddItem(OrderItem item)
    {
      _items.Add(item);
      UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveItem(OrderItem item)
    {
      _items.Remove(item);
      UpdatedAt = DateTime.UtcNow;
    }

    public static Order UpdateOrder(
    Guid orderId,
    Guid customerId,
    DateTime createdAt,
    OrderStatus status,
    PaymentMethod paymentMethod,
    decimal totalAmount,
    List<OrderItem> items,
    string? comments = null)
    {
      return new Order(
          id: new OrderId(orderId),
          customerId: customerId,
          createdAt: createdAt,
          status: status,
          paymentMethod: paymentMethod,
          totalAmount: totalAmount,
          items: items,
          comments: comments
      );
    }
  }
}
