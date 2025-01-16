using Application.Orders.GetByld;
using Domain.Entities;
using ErrorOr;
using MediatR;
using Orders.Common;

namespace Application.Customers.GetById;


internal sealed class GetOrderByIdQueryHandler : IRequestHandler<GetOrdersByIdQuery, ErrorOr<OrderResponse>>
{
  private readonly IOrderRepository _orderRepository;

  public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
  {
    _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
  }

  public async Task<ErrorOr<OrderResponse>> Handle(GetOrdersByIdQuery query, CancellationToken cancellationToken)
  {
    if (await _orderRepository.GetByIdAsync(new OrderId(query.Id)) is not Order order)
    {
      return Error.NotFound("Order.NotFound", "The order with the provide Id was not found.");
    }

    var orderResponse = new OrderResponse(
        Id: order.Id.Value,
        CreatedAt: order.CreatedAt,
        Status: order.Status,
        PaymentMethod: order.PaymentMethod,
        Comments: order.Comments,
        Items: order.Items
            .Select(item => new OrderItemResponse(
                ProductId: item.ProductId,
                Quantity: item.Quantity,
                UnitPrice: item.UnitPrice
            )).ToList());

    return orderResponse;
  }
}