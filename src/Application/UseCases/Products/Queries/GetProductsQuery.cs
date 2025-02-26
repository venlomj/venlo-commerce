using Application.Abstractions.Messaging;
using Application.DTOs.Products;
using SharedKernel;
using System;

namespace Application.UseCases.Products.Queries
{
    public class GetProductsQuery : IQuery<Result<PagedResult<ProductResponse>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string? SearchTerm { get; set; } // Search by name or description
        public string? Category { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? SortBy { get; set; }
        public string? SortOrder { get; set; }

        public GetProductsQuery(int page, int pageSize, string? searchTerm = null, string? category = null,
            decimal? minPrice = null, decimal? maxPrice = null, string? sortBy = null, string? sortOrder = null)
        {
            Page = page;
            PageSize = pageSize;
            SearchTerm = searchTerm;
            Category = category;
            MinPrice = minPrice;
            MaxPrice = maxPrice;
            SortBy = sortBy;
            SortOrder = sortOrder;
        }
    }
}