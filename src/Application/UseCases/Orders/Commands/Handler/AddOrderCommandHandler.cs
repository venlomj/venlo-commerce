

using NUlid;

namespace Application.UseCases.Orders.Commands.Handler
{
    public class AddOrderCommandHandler
    {
        private string GenerateOrderNumber()
        {
            var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            var ulid = Ulid.NewUlid().ToString()[10..];
            return $"ORD-{timestamp}{ulid}";
        }
    }
}
