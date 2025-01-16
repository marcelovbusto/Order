using Application.Orders.Update;
using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;
using System.Linq;

namespace Application.Customers.Update;

internal sealed class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, ErrorOr<Unit>>
{
  private readonly IOrderRepository _orderRepository;
  private readonly IUnitOfWork _unitOfWork;
  public UpdateOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
  {
    _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
    _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
  }
  public async Task<ErrorOr<Unit>> Handle(
      UpdateOrderCommand command,
      CancellationToken cancellationToken)
  {
    var orderId = new OrderId(command.Id);

    var order = await _orderRepository.GetByIdAsync(orderId);
    if (order is null)
    {
      return Error.NotFound(
          "Order.NotFound",
          "The order with the provided Id was not found."
      );
    }

    var domainItems = command.Items
        .Select(i => new OrderItem(
            productId: i.ProductId,
            quantity: i.Quantity,
            unitPrice: i.UnitPrice,
            orderId: i.OrderId
        ))
        .ToList();

    var updatedOrder = Order.UpdateOrder(
        orderId: command.Id,
        customerId: command.CustomerId,
        createdAt: command.CreatedAt,
        status: command.Status,
        paymentMethod: command.PaymentMethod,
        totalAmount: command.TotalAmount,
        items: domainItems,
        comments: command.Comments
    );

    _orderRepository.Update(updatedOrder);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return Unit.Value;
  }
}
