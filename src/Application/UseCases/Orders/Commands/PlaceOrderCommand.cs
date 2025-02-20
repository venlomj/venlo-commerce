using Application.Abstractions.Messaging;
using Application.DTOs.Products;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Orders;

namespace Application.UseCases.Orders.Commands
{
    public class PlaceOrderCommand : ICommand<OrderResponse>
    {
        //public PlaceOrderCommand(OrderRequest orderRequest, Guid productId)
        //{
        //    OrderRequest = orderRequest;
        //    ProductId = productId;
        //}

        //public OrderRequest OrderRequest { get; set; }
        //public Guid ProductId { get; set; }

        public PlaceOrderCommand(int quantity, Guid productId)
        {
            Quantity = quantity;
            ProductId = productId;
        }

        public int Quantity { get; set; }
        public Guid ProductId { get; set; }
    }
}
