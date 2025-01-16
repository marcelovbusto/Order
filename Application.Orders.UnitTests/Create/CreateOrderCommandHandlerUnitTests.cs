using Application.Customers.Create;
using Application.Orders.Create;
using Domain.Entities;
using Domain.Enums;
using Domain.Primitives;

namespace Application.Orders.UnitTests.Create
{
  public class CreateOrderCommandHandlerUnitTests
  {
    private readonly Mock<IOrderRepository> _mockOrderRepository;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly CreateOrderCommandHandler _handler;

    public CreateOrderCommandHandlerUnitTests()
    {
      _mockOrderRepository = new Mock<IOrderRepository>();
      _mockUnitOfWork = new Mock<IUnitOfWork>();

      _handler = new CreateOrderCommandHandler(_mockOrderRepository.Object, _mockUnitOfWork.Object);
    }

    [Fact]
    public async Task Handle_WhenItemUnitPriceIsZero_ShouldReturnValidationError()
    {
      // Arrange
      var command = new CreateOrderCommand(
          Guid.NewGuid(),           // CustomerId
          PaymentMethod.CreditCard, // PaymentMethod
          "Order with invalid item",// Comments
          new List<CreateOrderItemCommand>
          {
            new CreateOrderItemCommand(
                1,                          // ProductId
                1,                          // Quantity
                0m,                         // UnitPrice (inválido)
                new OrderId(Guid.NewGuid()) // OrderId
            )
          }
      );

      // Act
      var result = await _handler.Handle(command, default);

      // Assert
      result.IsError.Should().BeTrue();
      result.FirstError.Type.Should().Be(ErrorType.Validation);
      result.FirstError.Description.Should().Be("Unit price deve ser maior que zero."); // Verifica a mensagem exata
    }

  }
}
