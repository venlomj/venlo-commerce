using Domain.Common;

namespace Domain.Entities
{
    public class Category : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty!;
        public string? Description { get; set; }

        public List<Product> Products { get; set; } = [];
    }
}
