using Application.DTOs.Pictures;
using Application.DTOs.Products;
using AutoMapper;
using Domain.Documents;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories.Pictures;
using Domain.Repositories.Products;
using MediatR;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Products.Queries.Handlers
{
    public class GetProductsByCategoryQueryHandler : IRequestHandler<GetProductsByCategoryQuery, Result<IEnumerable<ProductResponse>>>
    {
        private readonly IProductsReaderRepository _productsReader;
        private readonly IMapper _mapper;
        private IMongoRepository<ProductImage> _productImageRepository;

        public GetProductsByCategoryQueryHandler(IProductsReaderRepository productsReader, IMapper mapper, IMongoRepository<ProductImage> productImageRepository)
        {
            _productsReader = productsReader;
            _mapper = mapper;
            _productImageRepository = productImageRepository;
        }

        public async Task<Result<IEnumerable<ProductResponse>>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
        {
            var products = await _productsReader.GetByCategoryIdAsync(request.CategoryId);
            if (products == null)
            {
                return new BusinessLogicException($"No Products found for the category with id, {request.CategoryId}.");
            }

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
