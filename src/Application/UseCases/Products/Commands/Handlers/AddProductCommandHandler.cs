using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Products;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories.Products;
using Domain.UoW;
using MediatR;

namespace Application.UseCases.Products.Commands.Handlers
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, ProductResponse>
    {
        private readonly IProductWriterRepository _productWriterRepository;
        private readonly IProductsReaderRepository _productReaderRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AddProductCommandHandler(IProductWriterRepository productWriterRepository, IMapper mapper, IUnitOfWork unitOfWork, IProductsReaderRepository productReaderRepository)
        {
            _productWriterRepository = productWriterRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _productReaderRepository = productReaderRepository;
        }

        public async Task<ProductResponse> Handle(AddProductCommand request, CancellationToken cancellationToken)
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
