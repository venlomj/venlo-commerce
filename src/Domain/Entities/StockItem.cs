using Domain.Common;

namespace Domain.Entities
{
    public class StockItem : BaseEntity
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }

        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
