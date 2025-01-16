using ErrorOr;

namespace Domain.DomainErrors
{
  public static partial class Errors
  {
    public static class Order
    {
      public static Error UnitPriceMustBeGreaterThanZero =>
            Error.Validation(code: "Order.UnitPrice.Invalid", description: ValidationMessages.UnitPriceMustBeGreaterThanZero);

      public static Error MustHaveAtLeastOneItem =>
          Error.Validation(code: "Order.Items.Missing", description: ValidationMessages.MustHaveAtLeastOneItem);
    }
  }
}
