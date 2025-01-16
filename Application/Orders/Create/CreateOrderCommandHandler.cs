using Application.Orders.Create;
using Domain.Entities;
using Domain.Enums;
using Domain.Primitives;
using ErrorOr;
using MediatR;
using Domain.DomainErrors;

namespace Application.Customers.Create;

public sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ErrorOr<Guid>>
{
  private readonly IOrderRepository _orderRepository;
  private readonly IUnitOfWork _unitOfWork;

  public CreateOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
  {
    _orderRepository = orderRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task<ErrorOr<Guid>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
  {
    
    if (command.Items == null || !command.Items.Any())
    {
      return Errors.Order.MustHaveAtLeastOneItem;
    }
  
    if (command.Items.Any(item => item.UnitPrice <= 0))
    {
      return Errors.Order.UnitPriceMustBeGreaterThanZero;
    }
    
    var orderItems = command.Items
            .Select(itemCommand => new OrderItem(
                productId: itemCommand.ProductId,
                quantity: itemCommand.Quantity,
                unitPrice: itemCommand.UnitPrice,
                orderId: itemCommand.OrderId
            ))
            .ToList();

    
    var totalAmount = orderItems.Sum(o => o.Quantity * o.UnitPrice);
   
    var order = new Order(
        id: new OrderId(Guid.NewGuid()),
        customerId: command.CustomerId,
        createdAt: DateTime.UtcNow,
        status: OrderStatus.Open,
        paymentMethod: command.PaymentMethod,
        totalAmount: totalAmount,
        items: orderItems,
        comments: command.Comments
    );
    
    _orderRepository.Add(order);
    
    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return order.Id.Value;
  }
}
