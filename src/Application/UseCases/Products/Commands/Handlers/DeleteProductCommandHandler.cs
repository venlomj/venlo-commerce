using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Products;
using AutoMapper;
using Domain.Exceptions;
using Domain.Repositories.Products;
using Domain.UoW;
using MediatR;
using SharedKernel;

namespace Application.UseCases.Products.Commands.Handlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<ProductResponse>>
    {
        private readonly IProductWriterRepository _productWriterRepository;
        private readonly IProductsReaderRepository _productReaderRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(IProductWriterRepository productWriterRepository, IMapper mapper, IUnitOfWork unitOfWork, IProductsReaderRepository productReaderRepository)
        {
            _productWriterRepository = productWriterRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _productReaderRepository = productReaderRepository;
        }

        public async Task<Result<ProductResponse>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                return new BusinessLogicException("Cannot delete product. Missing or invalid Id");

            var existingProduct = await _productReaderRepository.GetById(request.Id);
            if (existingProduct == null)
                return new BusinessLogicException($"Product with Id {request.Id} not found");

            _productWriterRepository.Delete(existingProduct);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var result = _mapper.Map<ProductResponse>(existingProduct);
            return result;
        }

    }
}
