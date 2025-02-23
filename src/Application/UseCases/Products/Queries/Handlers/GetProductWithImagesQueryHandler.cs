using Application.DTOs.Products;
using AutoMapper;
using Domain.Repositories.Products;
using MediatR;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Pictures;
using Domain.Documents;
using Domain.Repositories.Pictures;
using Domain.Exceptions;

namespace Application.UseCases.Products.Queries.Handlers
{
    public class GetProductWithImagesQueryHandler : IRequestHandler<GetProductWithImagesQuery, Result<IEnumerable<ProductResponse>>>
    {
        private readonly IProductsReaderRepository _productsReader;
        private readonly IMongoRepository<ProductImage> _productImageRepository;
        private readonly IMapper _mapper;

        public GetProductWithImagesQueryHandler(IProductsReaderRepository productsReader,
            IMapper mapper, IMongoRepository<ProductImage> productImageRepository)
        {
            _productsReader = productsReader;
            _mapper = mapper;
            _productImageRepository = productImageRepository;
        }

        public async Task<Result<IEnumerable<ProductResponse>>> Handle(GetProductWithImagesQuery request, CancellationToken cancellationToken)
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
