using Domain.Entities;
using ErrorOr;
using MediatR;
using Orders.Common;

namespace Application.Customers.GetAll;


internal sealed class GetAllOrderQueryHandler : IRequestHandler<GetAllOrdersQuery, ErrorOr<IReadOnlyList<OrderResponse>>>
{
  private readonly IOrderRepository _orderRepository;

  public GetAllOrderQueryHandler(IOrderRepository orderRepository)
  {
    _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
  }

  public async Task<ErrorOr<IReadOnlyList<OrderResponse>>> Handle(GetAllOrdersQuery query, CancellationToken cancellationToken)
  {
    IReadOnlyList<Order> orders = await _orderRepository.GetAll();

    return orders.Select(order =>
    {
      var itemResponses = order.Items
          .Select(item => new OrderItemResponse(
              item.ProductId,
              item.Quantity,
              item.UnitPrice
          )).ToList();

      return new OrderResponse(
          order.Id.Value,
          order.CreatedAt,
          order.Status,
          order.PaymentMethod,
          order.Comments,
          itemResponses
      );
    }).ToList();
  }
}