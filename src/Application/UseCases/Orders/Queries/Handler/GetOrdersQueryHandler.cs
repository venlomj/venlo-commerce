using Application.DTOs.Orders;
using Application.DTOs.Products; // Remove this if not needed
using AutoMapper;
using Domain.Entities;
using Domain.Repositories.Orders;
using MediatR;
using SharedKernel;
using System.Linq.Expressions;

namespace Application.UseCases.Orders.Queries.Handler
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, Result<PagedResult<OrderResponse>>>
    {
        private readonly IOrdersReaderRepository _ordersReader;
        private readonly IMapper _mapper;

        public GetOrdersQueryHandler(IMapper mapper, IOrdersReaderRepository ordersReader)
        {
            _mapper = mapper;
            _ordersReader = ordersReader;
        }

        public async Task<Result<PagedResult<OrderResponse>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            // Step 1: Build Filter Expression
            Expression<Func<Order, bool>>? filter = null;
            if (!string.IsNullOrEmpty(request.SearchTerm) || request.StartDate.HasValue || request.EndDate.HasValue ||
                request.MinTotalAmount.HasValue || request.MaxTotalAmount.HasValue)
            {
                var trimmedSearchTerm = request.SearchTerm?.Trim().ToLower();
                filter = o =>
                    (string.IsNullOrEmpty(trimmedSearchTerm) || o.OrderNumber.ToLower().Contains(trimmedSearchTerm)) &&
                    (!request.StartDate.HasValue || o.DateCreated >= request.StartDate) &&
                    (!request.EndDate.HasValue || o.DateCreated <= request.EndDate) &&
                    (!request.MinTotalAmount.HasValue || o.OrderLineItems.Sum(i => i.Price * i.Quantity) >= request.MinTotalAmount) &&
                    (!request.MaxTotalAmount.HasValue || o.OrderLineItems.Sum(i => i.Price * i.Quantity) <= request.MaxTotalAmount);
            }

            // Step 2: Apply Sorting Logic
            Func<IQueryable<Order>, IOrderedQueryable<Order>>? orderBy = null;
            if (!string.IsNullOrEmpty(request.SortBy))
            {
                orderBy = query => request.SortBy.ToLower() switch
                {
                    "datecreated" => request.SortOrder?.ToLower() == "desc"
                        ? query.OrderByDescending(o => o.DateCreated)
                        : query.OrderBy(o => o.DateCreated),
                    "totalamount" => request.SortOrder?.ToLower() == "desc"
                        ? query.OrderByDescending(o => o.OrderLineItems.Sum(i => i.Price * i.Quantity))
                        : query.OrderBy(o => o.OrderLineItems.Sum(i => i.Price * i.Quantity)),
                    _ => query.OrderBy(o => o.DateCreated), // Default sorting by DateCreated
                };
            }

            // Step 3: Fetch Data from Repository
            var totalCount = await _ordersReader.CountAsync(filter);
            var orders = await _ordersReader.GetFiltered(filter, orderBy, request.Page, request.PageSize);

            // Step 4: Map Orders to OrderResponse
            var orderResponses = _mapper.Map<List<OrderResponse>>(orders);

            // Step 5: Return Paged Result
            return new Result<PagedResult<OrderResponse>>(
                new PagedResult<OrderResponse>(orderResponses, totalCount, request.Page, request.PageSize));
        }
    }
}