using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;
using Application.DTOs.Products;
using SharedKernel;

namespace Application.UseCases.Products.Queries
{
    public class GetProductsPagedQuery : IQuery<Result<PagedResult<ProductResponse>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public GetProductsPagedQuery(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }

    }
}
