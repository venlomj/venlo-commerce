using Application.Abstractions.Messaging;
using Application.DTOs.Products;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Products.Queries
{
    public class GetProductsByCategoryQuery : IQuery<Result<IEnumerable<ProductResponse>>>
    {
        public GetProductsByCategoryQuery(Guid categoryId)
        {
            CategoryId = categoryId;
        }

        public Guid CategoryId { get; set; }

    }
}
