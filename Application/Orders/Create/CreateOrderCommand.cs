using Domain.Entities;
using Domain.Enums;
using ErrorOr;
using MediatR;

namespace Application.Orders.Create
{
  public record CreateOrderCommand(
        Guid CustomerId,
        PaymentMethod PaymentMethod,
        string? Comments,
        List<CreateOrderItemCommand> Items
   ) : IRequest<ErrorOr<Guid>>;

  public record CreateOrderItemCommand(
        int ProductId,
        int Quantity,
        decimal UnitPrice,
        OrderId OrderId
   );
}
