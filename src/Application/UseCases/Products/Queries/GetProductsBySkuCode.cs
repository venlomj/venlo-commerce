using Application.Abstractions.Messaging;
using Application.DTOs.Inventories;
using Application.DTOs.Products;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel;

namespace Application.UseCases.Products.Queries
{
    public class GetProductsBySkuCode : IQuery<Result<IEnumerable<ProductResponse>>>
    {
        public GetProductsBySkuCode(IEnumerable<string> skuCodes)
        {
            SkuCodes = skuCodes;
        }

        public IEnumerable<string> SkuCodes { get; set; }
    }
}

//public List<ProductResponse> getAllProductsBySkuCode(List<String> skuCode)
//{
//    List<Product> products = productRepository.findBySkuCodeIn(skuCode);

//    return products.stream().map(this::mapToProductResponse).toList();
//}