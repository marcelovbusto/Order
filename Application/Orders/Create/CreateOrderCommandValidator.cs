using Application.Orders.Create;
using FluentValidation;

namespace Application.Customers.Create;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        // to do
    }
}