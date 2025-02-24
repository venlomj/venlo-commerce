using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace Application.UseCases.Products.Queries.Handlers
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Result<ProductResponse>>
    {
        private readonly IProductsReaderRepository _productsReaderRepository;
        private readonly IMapper _mapper;
        private IMongoRepository<ProductImage> _productImageRepository;

        public GetProductQueryHandler(IProductsReaderRepository productsReaderRepository, IMapper mapper, IMongoRepository<ProductImage> productImageRepository)
        {
            _productsReaderRepository = productsReaderRepository;
            _mapper = mapper;
            _productImageRepository = productImageRepository;
        }

        public async Task<Result<ProductResponse>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _productsReaderRepository.GetById(request.Id);
            if (product == null)
            {
                return new BusinessLogicException($"Product with {request.Id} not found.");
            }

            var images = await _productImageRepository.GetAll();

            // Filter images by ProductId
            var productImages = images.Where(img => img.ProductId == product.Id).ToList();

            // Map Product entity to ProductResponse DTO using AutoMapper
            var productResponse = _mapper.Map<ProductResponse>(product);

            // Attach images to the DTO
            productResponse.Images = _mapper.Map<List<PictureResponse>>(productImages);

            return productResponse;
        }


        //public async Task<Result<ProductResponse>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        //{
        //    var product = await _productRepository.GetByIdAsync(request.Id);

        //    if (product == null)
        //    {
        //        // Returning a failure result
        //        return new Result<ProductResponse>(new BusinessLogicException($"Product with {request.Id} not found."));
        //    }

        //    var productResponse = _mapper.Map<ProductResponse>(product);

        //    // Returning a successful result
        //    return new Result<ProductResponse>(productResponse);
        //}
        //public async Task<Result<ProductResponse>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        //{
        //    var product = await _productRepository.GetByIdAsync(request.Id);
        //    //if (product == null)
        //    //{
        //    //    throw new BusinessLogicException($"Product with {request.Id} not found.");
        //    //}

        //    return _mapper.Map<ProductResponse>(product);
        //}
    }
}
