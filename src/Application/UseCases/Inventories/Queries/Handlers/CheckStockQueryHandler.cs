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
using Application.DTOs.Inventories;
using Domain.Repositories.Inventories;
using Domain.Entities;

namespace Application.UseCases.Inventories.Queries.Handlers
{
    public class CheckStockQueryHandler : IRequestHandler<CheckStockQuery, Result<IEnumerable<InventoryResponse>>>
    {
        private readonly IInventoriesReaderRepository _inventoriesReaderRepository;
        private readonly IProductsReaderRepository _productsReaderRepository;
        private readonly IMapper _mapper;

        public CheckStockQueryHandler(
            IInventoriesReaderRepository inventoriesReaderRepository,
            IProductsReaderRepository productsReaderRepository,
            IMapper mapper)
        {
            _inventoriesReaderRepository = inventoriesReaderRepository;
            _productsReaderRepository = productsReaderRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<InventoryResponse>>> Handle(CheckStockQuery request, CancellationToken cancellationToken)
        {
            var products = await _productsReaderRepository.MultipleByValue(request.SkuCodes);

            var stockItems = await _inventoriesReaderRepository.MultipleByValue(request.SkuCodes);

            var result = products.Select(product =>
            {
                var stock = stockItems.FirstOrDefault(s => s.ProductId == product.Id);
                return new InventoryResponse
                {
                    SkuCode = product.SkuCode,
                    IsInStock = stock?.Quantity > 0
                };
            });

            return result.ToList();
        }
    }

}
