using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;
using Application.DTOs.Orders;
using Application.DTOs.Products;
using SharedKernel;

namespace Application.UseCases.Orders.Queries
{
    public class GetOrdersQuery(
        int page = 1,
        int pageSize = 10,
        string? searchTerm = null,
        DateTimeOffset? startDate = null,
        DateTimeOffset? endDate = null,
        decimal? minTotalAmount = null,
        decimal? maxTotalAmount = null,
        string? sortBy = null,
        string? sortOrder = null)
        : IQuery<Result<PagedResult<OrderResponse>>> 
    {
        public int Page { get; set; } = page;
        public int PageSize { get; set; } = pageSize;
        public string? SearchTerm { get; set; } = searchTerm;
        public DateTimeOffset? StartDate { get; set; } = startDate;
        public DateTimeOffset? EndDate { get; set; } = endDate;
        public decimal? MinTotalAmount { get; set; } = minTotalAmount;
        public decimal? MaxTotalAmount { get; set; } = maxTotalAmount;
        public string? SortBy { get; set; } = sortBy;
        public string? SortOrder { get; set; } = sortOrder;
    }

}
