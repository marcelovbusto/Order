using Application.Orders.Delete;
using FluentValidation;

namespace Application.Customers.Delete;

public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator()
    {
        RuleFor(r => r.Id)
            .NotEmpty();
    }
}