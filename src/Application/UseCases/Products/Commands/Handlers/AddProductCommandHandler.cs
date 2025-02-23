using Application.DTOs.Pictures;
using Application.DTOs.Products;
using Application.Services;
using AutoMapper;
using Domain.Documents;
using Domain.Entities;
using Domain.Repositories.Pictures;
using Domain.Repositories.Products;
using Domain.UoW;
using MediatR;
using Microsoft.AspNetCore.Http;
using SharedKernel;

namespace Application.UseCases.Products.Commands.Handlers
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Result<ProductResponse>>
    {
        private readonly IProductWriterRepository _productWriterRepository;
        private readonly IProductsReaderRepository _productReaderRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMongoRepository<ProductImage> _imageRepository;
        private readonly IImageService _imageService;

        public AddProductCommandHandler(IProductWriterRepository productWriterRepository, IMapper mapper, IUnitOfWork unitOfWork, IProductsReaderRepository productReaderRepository, IMongoRepository<ProductImage> imageRepository, IImageService imageService)
        {
            _productWriterRepository = productWriterRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _productReaderRepository = productReaderRepository;
            _imageRepository = imageRepository;
            _imageService = imageService;
        }

        public async Task<Result<ProductResponse>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request.ProductRequest);

            product.Id = Guid.NewGuid();
            product.SkuCode = $"SKU-{Guid.NewGuid()}";
            product.DateCreated = DateTimeOffset.UtcNow;


            var id = await _productWriterRepository.Add(product);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var result = await _productReaderRepository.GetById(id);

            // Map the saved product back to a response DTO
            return _mapper.Map<ProductResponse>(result);
        }

    }
}
