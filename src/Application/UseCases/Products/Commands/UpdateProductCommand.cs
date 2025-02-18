using Application.Abstractions.Messaging;
using Application.DTOs.Products;
using SharedKernel;

namespace Application.UseCases.Products.Commands
{
    public class UpdateProductCommand : ICommand<Result<ProductResponse>>
    {
        public UpdateProductCommand(Guid id, ProductRequest productRequest)
        {
            Id = id;
            ProductRequest = productRequest;
        }

        public Guid Id { get; set; }
        public ProductRequest ProductRequest { get; set; }
    }
}
