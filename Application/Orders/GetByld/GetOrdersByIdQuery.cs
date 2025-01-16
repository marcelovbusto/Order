using ErrorOr;
using MediatR;
using Orders.Common;

namespace Application.Orders.GetByld
{
  public record GetOrdersByIdQuery(Guid Id) : IRequest<ErrorOr<OrderResponse>>;
}
