﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Orders
{
    public class OrderRequest
    {
        public int Quantity { get; set; }
    }


    //public class OrderRequest
    //{
    //    public Guid ProductId { get; set; }
    //    public List<OrderLineItemDto> OrderLineItemsDtoList { get; set; } = new();
    //}
}
