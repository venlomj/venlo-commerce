using Application.DTOs.Orders;
using AutoMapper;
using Domain.Models;
using Domain.Repositories.Orders;
using Domain.Utils;
using MediatR;

namespace Application.UseCases.Invoice.Query.Handler
{
    public class GetInvoiceQueryHandler : IRequestHandler<GetInvoiceQuery, OrderResponse>
    {
        private readonly IOrdersReaderRepository _ordersReader;
        private readonly IMapper _mapper;

        public GetInvoiceQueryHandler(
            IMapper mapper, IOrdersReaderRepository ordersReader)
        {
            _mapper = mapper;
            _ordersReader = ordersReader;
        }

        public async Task<OrderResponse> Handle(GetInvoiceQuery request, CancellationToken cancellationToken)
        {
            var order = await _ordersReader.GetById(request.OrderId);

            // Maak de factuur aan zonder handmatige berekeningen
            var invoice = new Domain.Models.Invoice
            {
                InvoiceNumber = order.OrderNumber,
                DateCreated = order.DateCreated,
                LineItems = order.OrderLineItems.Select(item => new InvoiceLineItem
                {
                    ProductName = item.Product.Name,
                    SkuCode = item.Product.SkuCode,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.Price
                }).ToList() // TotalPrice wordt nu automatisch berekend
            };

            var filePath = $"{invoice.InvoiceNumber}_Invoice.pdf";
            PdfUtility.GeneratePdf(filePath, invoice);

            return _mapper.Map<OrderResponse>(order);
        }
    }
}