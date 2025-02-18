using Application.DTOs.Products;
using AutoMapper;
using Domain.Exceptions;
using Domain.Repositories.Products;
using MediatR;
using SharedKernel;

namespace Application.UseCases.Products.Queries.Handlers
{
    public class GetProductsBySkuCodeHandler : IRequestHandler<GetProductsBySkuCode, Result<IEnumerable<ProductResponse>>>
    {
        private readonly IProductsReaderRepository _productsReaderRepository;
        private readonly IMapper _mapper;

        public GetProductsBySkuCodeHandler(IProductsReaderRepository productsReaderRepository,
            IMapper mapper)
        {
            _productsReaderRepository = productsReaderRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<ProductResponse>>> Handle(GetProductsBySkuCode request, CancellationToken cancellationToken)
        {
            if (request.SkuCodes == null || !request.SkuCodes.Any())
            {
                return new BusinessLogicException(
                        "Missing SKU code",
                        "No SKU codes were provided for the query.",
                        new []{ "SKU codes cannot be empty." }
                    );
            }

            var product = await _productsReaderRepository.MultipleByValue(request.SkuCodes);

            if (product == null || !product.Any())
            {
                return new BusinessLogicException(
                    "Products not found",
                    "The provided SKU codes did not match any products.",
                    request.SkuCodes
                    );
            }

            return _mapper.Map<Result<IEnumerable<ProductResponse>>>(product);
        }
    }
}
