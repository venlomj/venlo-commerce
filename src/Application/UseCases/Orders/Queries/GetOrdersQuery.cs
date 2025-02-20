using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;
using Application.DTOs.Orders;

namespace Application.UseCases.Orders.Queries
{
    public class GetOrdersQuery : IQuery<IEnumerable<OrderResponse>>
    {
    }
}
