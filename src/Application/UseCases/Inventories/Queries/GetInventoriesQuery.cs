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
    public class GetInventoriesQuery : IQuery<IEnumerable<InventoryResponse>>, IRequest<IEnumerable<ProductResponse>>
    {
        public GetInventoriesQuery(IEnumerable<string> skuCodes)
        {
            SkuCodes = skuCodes;
        }

        public IEnumerable<string> SkuCodes { get; set; }
    }

}
