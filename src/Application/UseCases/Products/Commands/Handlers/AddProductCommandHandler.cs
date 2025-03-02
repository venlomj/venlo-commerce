using Application.DTOs.Products;
using Application.Services;
using AutoMapper;
using Domain.Documents;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories.Categories;
using Domain.Repositories.Pictures;
using Domain.Repositories.Products;
using Domain.UoW;
using MediatR;
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
        private readonly ICategoryReaderRepository _categoryReader;

        public AddProductCommandHandler(IProductWriterRepository productWriterRepository, IMapper mapper, IUnitOfWork unitOfWork, IProductsReaderRepository productReaderRepository, IMongoRepository<ProductImage> imageRepository, IImageService imageService, ICategoryReaderRepository categoryReader)
        {
            _productWriterRepository = productWriterRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _productReaderRepository = productReaderRepository;
            _imageRepository = imageRepository;
            _categoryReader = categoryReader;
        }

        public async Task<Result<ProductResponse>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryReader.GetById(request.ProductRequest.CategoryId);
            if (category == null)
            {
                throw new BusinessLogicException("Category not found");
            }

            var product = _mapper.Map<Product>(request.ProductRequest);

            product.Id = Guid.NewGuid();
            product.SkuCode = $"SKU-{Guid.NewGuid()}";
            product.DateCreated = DateTimeOffset.UtcNow;
            product.CategoryId = category.Id;


            var id = await _productWriterRepository.Add(product);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var result = await _productReaderRepository.GetById(id);

            // Map the saved product back to a response DTO
            return _mapper.Map<ProductResponse>(result);
        }

    }
}
