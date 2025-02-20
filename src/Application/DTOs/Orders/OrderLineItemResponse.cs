namespace Application.DTOs.Orders;

public class OrderLineItemResponse
{
    public Guid Id { get; set; }
    public string ProductName { get; set; }
    public string SkuCode { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
}