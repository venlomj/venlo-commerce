using Application.DTOs.Pictures;
using Application.DTOs.Products;
using AutoMapper;
using Domain.Documents;
using Domain.Repositories.Pictures;
using Domain.Repositories.Products;
using MediatR;
using SharedKernel;

namespace Application.UseCases.Products.Queries.Handlers
{
    public class GetProductsPagedQueryHandler : IRequestHandler<GetProductsPagedQuery, Result<PagedResult<ProductResponse>>>
    {
        private readonly IProductsReaderRepository _productsReader;
        private readonly IMongoRepository<ProductImage> _productImageRepository;
        private readonly IMapper _mapper;

        public GetProductsPagedQueryHandler(IProductsReaderRepository productsReader,
            IMapper mapper, IMongoRepository<ProductImage> productImageRepository)
        {
            _productsReader = productsReader;
            _mapper = mapper;
            _productImageRepository = productImageRepository;
        }

        public async Task<Result<PagedResult<ProductResponse>>> Handle(GetProductsPagedQuery request, CancellationToken cancellationToken)
        {
            var totalCount = await _productsReader.CountAsync();
            var products = await _productsReader.GetPagedAsync(request.Page, request.PageSize);

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
            }).ToList(); // Ensure it's a List<ProductResponse>

            // Return a properly structured Result<PagedResult<ProductResponse>>
            return new Result<PagedResult<ProductResponse>>(new PagedResult<ProductResponse>(productResponses, totalCount, request.Page, request.PageSize));
        }

    }
}
