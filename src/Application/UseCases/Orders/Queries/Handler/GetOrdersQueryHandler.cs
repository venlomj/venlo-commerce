using Application.DTOs.Orders;
using AutoMapper;
using Domain.Repositories.Orders;
using Domain.Repositories.Products;
using MediatR;

namespace Application.UseCases.Orders.Queries.Handler
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IEnumerable<OrderResponse>>
    {
        private readonly IOrdersReaderRepository _ordersReader;
        private readonly IMapper _mapper;

        public GetOrdersQueryHandler(IMapper mapper, IOrdersReaderRepository ordersReader)
        {
            _mapper = mapper;
            _ordersReader = ordersReader;
        }

        public async Task<IEnumerable<OrderResponse>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _ordersReader.GetAll();

            return _mapper.Map<IEnumerable<OrderResponse>>(orders);
        }
    }
}
