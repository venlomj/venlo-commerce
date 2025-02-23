using Application.Abstractions.Messaging;
using Application.DTOs.Products;
using SharedKernel;

namespace Application.UseCases.Products.Queries
{
    public class GetProductWithImagesQuery : IQuery<Result<IEnumerable<ProductResponse>>>;
}
