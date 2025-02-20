

using Application.DTOs.Orders;
using Application.UseCases.Inventories.Queries;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories.Inventories;
using Domain.Repositories.Orders;
using Domain.Repositories.Products;
using Domain.UoW;
using MediatR;
using NUlid;
using SharedKernel;

namespace Application.UseCases.Orders.Commands.Handler
{
    public class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand, OrderResponse>
    {
        private readonly IOrderWriterRepository _orderWriter;
        private readonly IInventoriesReaderRepository _inventoriesReader;
        private readonly IInventoriesWriterRepository _inventoriesWriter;
        private readonly IProductsReaderRepository _productsReader;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PlaceOrderCommandHandler(IOrderWriterRepository orderWriter, IInventoriesReaderRepository inventoriesReader, IProductsReaderRepository productsReader, IMapper mapper, IUnitOfWork unitOfWork, IInventoriesWriterRepository inventoriesWriter)
        {
            _orderWriter = orderWriter;
            _inventoriesReader = inventoriesReader;
            _productsReader = productsReader;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _inventoriesWriter = inventoriesWriter;
        }

        public async Task<OrderResponse> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
        {
            var productExists = await _productsReader.Exists(request.ProductId);
            if (!productExists)
            {
                throw new BusinessLogicException($"The specified product with id {request.ProductId} does not exist in the database.");
            }

            var stockItem = await _inventoriesReader.GetByProductIdAsync(request.ProductId);
            if (stockItem == null || stockItem.Quantity <= 0)
            {
                throw new BusinessLogicException("Product is out of stock.");
            }

            if (stockItem?.Product == null)
            {
                throw new BusinessLogicException("Product associated with stock item not found.");
            }

            var requestedQuantity = request.Quantity;

            if (requestedQuantity > stockItem.Quantity)
            {
                throw new BusinessLogicException("Insufficient stock available.");
            }

            stockItem.Quantity -= requestedQuantity;
            await _inventoriesWriter.Update(stockItem);

            var order = new Order
            {
                Id = Guid.NewGuid(),
                OrderNumber = GenerateOrderNumber(),
                DateCreated = DateTimeOffset.UtcNow,
                OrderLineItems = new List<OrderLineItem>
                {
                    new OrderLineItem
                    {
                        Id = Guid.NewGuid(),
                        Quantity = requestedQuantity,
                        Price = stockItem.Product.Price,
                        ProductId = request.ProductId
                    }
                }
            };

            await _orderWriter.Add(order);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<OrderResponse>(order);
        }


        //public async Task<OrderResponse> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
        //{
        //    var productExists = await _productsReader.Exists(request.ProductId);
        //    // Check if the product exists
        //    if (!productExists)
        //    {
        //        throw new BusinessLogicException($"The specified product with id {request.ProductId} does not exist in the database.");
        //    }

        //    // Check stock availability
        //    var stockItem = await _inventoriesReader.GetByProductIdAsync(request.ProductId);
        //    if (stockItem == null || stockItem.Quantity <= 0)
        //    {
        //        throw new BusinessLogicException("Product is out of stock.");
        //    }

        //    // Ensure Product is loaded and available
        //    if (stockItem?.Product == null)
        //    {
        //        throw new BusinessLogicException("Product associated with stock item not found.");
        //    }

        //    // Calculate requested quantity
        //    var requestedQuantity = request.OrderRequest.OrderLineItemsDtoList.Sum(item => item.Quantity);

        //    // Verify if requested quantity is available in stock
        //    if (requestedQuantity > stockItem.Quantity)
        //    {
        //        throw new BusinessLogicException("Insufficient stock available.");
        //    }

        //    // Deduct stock
        //    stockItem.Quantity -= requestedQuantity;
        //    await _inventoriesWriter.Update(stockItem);

        //    var order = new Order
        //    {
        //        Id = Guid.NewGuid(),
        //        OrderNumber = GenerateOrderNumber(),
        //        DateCreated = DateTimeOffset.UtcNow,
        //        OrderLineItems = request.OrderRequest.OrderLineItemsDtoList.Select(item => new OrderLineItem
        //        {
        //            Id = Guid.NewGuid(),
        //            Quantity = item.Quantity,
        //            Price = stockItem.Product.Price,
        //            ProductId = request.ProductId
        //        }).ToList()
        //    };

        //    await _orderWriter.Add(order);
        //    await _unitOfWork.SaveChangesAsync(cancellationToken);

        //    return _mapper.Map<OrderResponse>(order);
        //}


        private string GenerateOrderNumber()
        {
            var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            var ulid = Ulid.NewUlid().ToString()[10..];
            return $"ORD-{timestamp}{ulid}";
        }
    }
}
