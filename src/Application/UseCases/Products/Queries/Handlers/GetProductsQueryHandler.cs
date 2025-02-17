using Application.Abstractions.Messaging;
using Application.DTOs.Products;
using AutoMapper;
using Domain.Repositories.Products;
using MediatR;
using SharedKernel;

namespace Application.UseCases.Products.Queries.Handlers
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, Result<IEnumerable<ProductResponse>>>
    {
        private readonly IProductsReaderRepository _productsReaderRepository;
        private readonly IMapper _mapper;

        public GetProductsQueryHandler(IProductsReaderRepository productsReaderRepository,
            IMapper mapper)
        {
            _productsReaderRepository = productsReaderRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<ProductResponse>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productsReaderRepository.GetAll();

            return _mapper.Map<List<ProductResponse>>(products);
        }
    }
}
