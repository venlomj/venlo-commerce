using Application.DTOs.categories;
using Application.DTOs.Inventories;
using Application.DTOs.Orders;
using Application.DTOs.Pictures;
using Application.DTOs.Products;
using Application.DTOs.Roles;
using Application.DTOs.UserRoles;
using Application.DTOs.Users;
using AutoMapper;
using Domain.Documents;
using Domain.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductResponse>();
            CreateMap<ProductRequest, Product>();
            CreateMap<Category, CategoryResponse>();


            // Mapping for IFormFile to byte[]
            CreateMap<IFormFile, byte[]>().ConvertUsing<FormFileToByteArrayConverter>();

            // Your other mappings
            CreateMap<PictureRequest, ProductImage>();
            CreateMap<ProductImage, PictureResponse>();

            // Inventory Mappings
            CreateMap<StockItem, InventoryResponse>();
            //CreateMap<StockItem, InventoryResponse>()
            //    .ForMember(dest => dest.SkuCode, opt => opt.MapFrom(src => src.Product.SkuCode))
            //    .ForMember(dest => dest.IsInStock, opt => opt.MapFrom(src => src.Quantity > 0));
            // CreateMap<Product, InventoryResponse>();

            // Mapping OrderLineItem to OrderLineItemResponse
            CreateMap<OrderLineItem, OrderLineItemResponse>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.SkuCode, opt => opt.MapFrom(src => src.Product.SkuCode))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Price * src.Quantity));

            // Mapping Order to OrderResponse
            CreateMap<Order, OrderResponse>()
                .ForMember(dest => dest.OrderLineItems, opt => opt.MapFrom(src => src.OrderLineItems))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.OrderLineItems.Sum(item => item.Price * item.Quantity)));

            CreateMap<OrderResponse, Invoice>()
                .ForMember(dest => dest.InvoiceNumber, opt => opt.MapFrom(src => src.OrderNumber))
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.DateCreated))
                .ForMember(dest => dest.LineItems, opt => opt.MapFrom(src => src.OrderLineItems));

            CreateMap<OrderLineItemResponse, InvoiceLineItem>()
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.UnitPrice * src.Quantity));

            // Mapping for Role and UserRole
            CreateMap<Role, RoleResponse>();

            // User Role Mapping
            CreateMap<UserRole, UserRoleResponse>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name));
            // User Mappings
            CreateMap<User, UserResponse>();
            CreateMap<UserRequest, User>();
            CreateMap<LoginRequest, User>();
        }
    }
}
