using ErrorOr;
using MediatR;

namespace Application.Orders.Delete
{
  public record DeleteOrderCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
}
