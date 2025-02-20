using Application.Abstractions.Messaging;
using Application.DTOs.Products;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Inventories;
using MediatR;

namespace Application.UseCases.Inventories.Queries
{
    public class CheckStockQuery : IQuery<Result<IEnumerable<InventoryResponse>>>
    {
        public CheckStockQuery(IEnumerable<string> skuCodes)
        {
            SkuCodes = skuCodes;
        }

        public IEnumerable<string> SkuCodes { get; set; }
    }

}
