using Domain.Common;

namespace Domain.Entities;

public class Product : BaseEntity
{
    public Guid Id { get; set; }
    public string SkuCode { get; set; } = string.Empty!;
    public string Name { get; set; } = string.Empty!;
    public string? Description { get; set; }
    public decimal Price { get; set; }

    public int CategoryId { get; set; }
    public Category? Category { get; set; }
}
