using FluentValidation;
using Application.UseCases.Products.Commands;
using Domain.Repositories.Categories;
using Domain.Repositories.Products;

namespace Application.Validators
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        private readonly ICategoryReaderRepository _categoryReader;
        private readonly IProductsReaderRepository _productsReader;

        public UpdateProductCommandValidator(
            ICategoryReaderRepository categoryReader,
            IProductsReaderRepository productsReader)
        {
            _categoryReader = categoryReader;
            _productsReader = productsReader;

            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("Product ID is required.")
                .MustAsync(async (id, cancellationToken) => await _productsReader.Exists(id))
                .WithMessage("Product does not exist.");

            RuleFor(c => c.ProductRequest.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(3, 100).WithMessage("Name must be between 3 and 100 characters.");

            RuleFor(c => c.ProductRequest.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(c => c.ProductRequest.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(c => c.ProductRequest.CategoryId)
                .MustAsync(async (categoryId, cancellationToken) => await _categoryReader.Exists(categoryId))
                .WithMessage("Category does not exist.");
        }
    }
}