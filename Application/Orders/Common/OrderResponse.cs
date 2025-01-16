using Domain.Enums;

namespace Orders.Common;

public record OrderResponse(
Guid Id,
DateTime CreatedAt,
OrderStatus Status,
PaymentMethod PaymentMethod,
string? Comments,
List<OrderItemResponse> Items);

public record OrderItemResponse(
    int ProductId,
    int Quantity,
    decimal UnitPrice);