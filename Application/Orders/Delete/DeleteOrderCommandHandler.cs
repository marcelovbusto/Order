using Application.Orders.Delete;
using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Customers.Delete;

internal sealed class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, ErrorOr<Unit>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<ErrorOr<Unit>> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        if (await _orderRepository.GetByIdAsync(new OrderId(command.Id)) is not Order order)
        {
            return Error.NotFound("Customer.NotFound", "The customer with the provide Id was not found.");
        }

        _orderRepository.Delete(order);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
