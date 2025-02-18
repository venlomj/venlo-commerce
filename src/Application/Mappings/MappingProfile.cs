using Application.DTOs.Inventories;
using Application.DTOs.Products;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductResponse>();
            CreateMap<ProductRequest, Product>();

            // Inventory Mappings
            CreateMap<StockItem, InventoryResponse>();
        }
    }
}
