using Application.DTOs.Products;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories.Products;
using Domain.UoW;
using MediatR;

namespace Application.UseCases.Products.Commands.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductResponse>
    {
        private readonly IProductWriterRepository _productWriterRepository;
        private readonly IProductsReaderRepository _productReaderRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductCommandHandler(IProductWriterRepository productWriterRepository, IMapper mapper, IUnitOfWork unitOfWork, IProductsReaderRepository productReaderRepository)
        {
            _productWriterRepository = productWriterRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _productReaderRepository = productReaderRepository;
        }

        public async Task<ProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                throw new BusinessLogicException("Cannot update product. Missing or invalid Id");

            var existingProduct = await _productReaderRepository.GetById(request.Id);
            if (existingProduct == null)
                throw new BusinessLogicException($"Product with Id {request.Id} not found");


            existingProduct.Name = request.ProductRequest.Name;
            existingProduct.Description = request.ProductRequest.Description;
            existingProduct.Price = request.ProductRequest.Price;
            existingProduct.DateModified = DateTimeOffset.UtcNow;

            var result = await _productWriterRepository.Update(existingProduct);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ProductResponse>(result);
        }

    }
}
