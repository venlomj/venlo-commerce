using Application.DTOs.Products;
using AutoMapper;
using Domain.Repositories.Products;
using MediatR;
using SharedKernel;
using Application.DTOs.Pictures;
using Domain.Documents;
using Domain.Repositories.Pictures;

namespace Application.UseCases.Products.Queries.Handlers
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, Result<IEnumerable<ProductResponse>>>
    {
        private readonly IProductsReaderRepository _productsReader;
        private readonly IMongoRepository<ProductImage> _productImageRepository;
        private readonly IMapper _mapper;

        public GetProductsQueryHandler(IProductsReaderRepository productsReader,
            IMapper mapper, IMongoRepository<ProductImage> productImageRepository)
        {
            _productsReader = productsReader;
            _mapper = mapper;
            _productImageRepository = productImageRepository;
        }

        public async Task<Result<IEnumerable<ProductResponse>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productsReader.GetAll();

            var images = await _productImageRepository.GetAll();

            // Map images to products using AutoMapper
            var productResponses = products.Select(product =>
            {
                // Filter images by ProductId
                var productImages = images.Where(img => img.ProductId == product.Id).ToList();

                // Map Product entity to ProductResponse DTO using AutoMapper
                var productResponse = _mapper.Map<ProductResponse>(product);

                // Attach images to the DTO
                productResponse.Images = _mapper.Map<List<PictureResponse>>(productImages);

                return productResponse;
            });

            return new Result<IEnumerable<ProductResponse>>(productResponses);
        }
    }
}
