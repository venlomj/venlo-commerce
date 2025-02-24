using Application.DTOs.Pictures;

namespace Application.DTOs.Products
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string SkuCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<PictureResponse> Images { get; set; } = [];
    }
}
