using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Products;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Domain.UoW;
using MediatR;

namespace Application.UseCases.Products.Commands.Handlers
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, ProductResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AddProductCommandHandler(IProductRepository productRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductResponse> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request.ProductRequest);

            product.Id = Guid.NewGuid();
            product.SkuCode = $"SKU-{Guid.NewGuid()}";
            product.DateCreated = DateTimeOffset.UtcNow;

            var result = await _productRepository.AddAsync(product);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ProductResponse>(result);
        }
    }
}
