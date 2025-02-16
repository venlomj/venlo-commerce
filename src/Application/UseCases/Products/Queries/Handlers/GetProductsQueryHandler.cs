using Application.Abstractions.Messaging;
using Application.DTOs.Products;
using AutoMapper;
using Domain.Repositories;
using MediatR;
using SharedKernel;

namespace Application.UseCases.Products.Queries.Handlers
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, Result<IEnumerable<ProductResponse>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<ProductResponse>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync();

            return _mapper.Map<List<ProductResponse>>(products);
        }
    }
}
