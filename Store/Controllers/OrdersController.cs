using Application.Customers.GetAll;
using Application.Orders.Create;
using Application.Orders.Delete;
using Application.Orders.GetByld;
using Application.Orders.Update;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;

namespace Store.Controllers
{

  [Route("orders")]
  public class Orders : ApiController
  {
    private readonly ISender _mediator;

    public Orders(ISender mediator)
    {
      _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]

    public async Task<IActionResult> GetAll()
    {
      var ordersResult = await _mediator.Send(new GetAllOrdersQuery());

      return ordersResult.Match(
        orders => Ok(orders),
            errors => Problem(errors)
            );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
      var ordersResult = await _mediator.Send(new GetOrdersByIdQuery(id));

      return ordersResult.Match(
          order => Ok(order),
          errors => Problem(errors)
      );
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
    {
      var createResult = await _mediator.Send(command);

      return createResult.Match(
          orderId => Ok(orderId),
          errors => Problem(errors)
      );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateOrderCommand command)
    {
      if (command.Id != id)
      {
        List<Error> errors = new()
            {
                Error.Validation("Order.UpdateInvalid", "The request Id does not match with the url Id.")
            };
        return Problem(errors);
      }

      var updateResult = await _mediator.Send(command);

      return updateResult.Match(
          orderId => NoContent(),
          errors => Problem(errors)
      );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
      var deleteResult = await _mediator.Send(new DeleteOrderCommand(id));

      return deleteResult.Match(
          orderId => NoContent(),
          errors => Problem(errors)
      );
    }
  }
}
