using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;
using Application.DTOs.Orders;

namespace Application.UseCases.Invoice.Query
{
    public class GetInvoiceQuery : IQuery<OrderResponse>
    {
        public GetInvoiceQuery(Guid orderId)
        {
            OrderId = orderId;
        }

        public Guid OrderId { get; set; }
    }
}
