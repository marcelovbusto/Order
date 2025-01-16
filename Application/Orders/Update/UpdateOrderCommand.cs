using Domain.Entities;
using Domain.Enums;
using ErrorOr;
using MediatR;

namespace Application.Orders.Update
{
  public record UpdateOrderCommand(
         Guid Id,
         Guid CustomerId,
         DateTime CreatedAt,
         OrderStatus Status,
         PaymentMethod PaymentMethod,
         decimal TotalAmount,
         List<UpdateOrderItemCommand> Items,
         string? Comments
     ) : IRequest<ErrorOr<Unit>>;

  public record UpdateOrderItemCommand(
      int ProductId,
      int Quantity,
      decimal UnitPrice,
      OrderId OrderId
  );
}
