using Domain.Common;

namespace Domain.Entities;

public class Product : BaseEntity
{
    public Guid Id { get; set; }
    public string SkuCode { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }

}
