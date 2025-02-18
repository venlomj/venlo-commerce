using Domain.Common;

namespace Domain.Entities
{
    public class Order: BaseEntity
    {
        public Guid Id { get; set; }
        public string OrderNumber { get; set; }
        public List<OrderLineItem> OrderLineItems { get; set; } = new();

    }
}
