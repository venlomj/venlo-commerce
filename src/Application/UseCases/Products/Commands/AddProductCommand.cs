using Application.Abstractions.Messaging;
using Application.DTOs.Products;
using SharedKernel;

namespace Application.UseCases.Products.Commands
{
    public class AddProductCommand : ICommand<Result<ProductResponse>>
    {
        public AddProductCommand(ProductRequest productRequest)
        {
            ProductRequest = productRequest;
        }

        public ProductRequest ProductRequest { get; set; }


    }
}
