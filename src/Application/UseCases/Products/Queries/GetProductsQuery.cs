using Application.Abstractions.Messaging;
using Application.DTOs.Products;
using SharedKernel;

public sealed record GetProductsQuery : IQuery<Result<IEnumerable<ProductResponse>>>;