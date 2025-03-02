using FluentValidation;
using Application.UseCases.Products.Commands;
using Domain.Repositories.Categories;

namespace Application.Validators
{
    public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
    {
        private readonly ICategoryReaderRepository _categoryReader;

        public AddProductCommandValidator(ICategoryReaderRepository categoryReader)
        {
            _categoryReader = categoryReader;

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