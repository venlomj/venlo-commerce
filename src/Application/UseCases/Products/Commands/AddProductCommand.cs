﻿using Application.Abstractions.Messaging;
using Application.DTOs.Products;

namespace Application.UseCases.Products.Commands
{
    public class AddProductCommand : ICommand<ProductResponse>
    {
        public AddProductCommand(ProductRequest productRequest)
        {
            ProductRequest = productRequest;
        }

        public ProductRequest ProductRequest { get; set; }


    }
}
