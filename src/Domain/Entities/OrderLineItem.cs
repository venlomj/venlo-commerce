using Domain.Common;

namespace Domain.Entities
{
    public class OrderLineItem : BaseEntity
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        // Foreign Keys
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }

        // Navigation Property
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
