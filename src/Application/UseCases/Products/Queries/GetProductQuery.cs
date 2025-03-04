using Application.Abstractions.Messaging;
using Application.DTOs.Products;
using SharedKernel;

namespace Application.UseCases.Products.Queries
{
    public class GetProductQuery : IQuery<Result<ProductResponse>>
    {
        public GetProductQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

    }
}
