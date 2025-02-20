using System;
using System.Collections.Generic;

namespace Application.DTOs.Orders
{
    public record OrderResponse
    {
        public string OrderNumber { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public List<OrderLineItemResponse> OrderLineItems { get; set; }
        public decimal TotalAmount { get; set; }

        // Parameterless constructor for AutoMapper to work
        public OrderResponse() { }

        // Constructor with parameters for direct initialization
        public OrderResponse(string orderNumber, DateTimeOffset dateCreated, List<OrderLineItemResponse> orderLineItems, decimal totalAmount)
        {
            OrderNumber = orderNumber;
            DateCreated = dateCreated;
            OrderLineItems = orderLineItems;
            TotalAmount = totalAmount;
        }
    }
}