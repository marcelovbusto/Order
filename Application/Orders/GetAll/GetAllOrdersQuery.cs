using Orders.Common;
using ErrorOr;
using MediatR;

namespace Application.Customers.GetAll;

public record GetAllOrdersQuery() : IRequest<ErrorOr<IReadOnlyList<OrderResponse>>>;