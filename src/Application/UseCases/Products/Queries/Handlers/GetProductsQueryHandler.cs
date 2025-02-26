using System.Linq.Expressions;
using Application.DTOs.Pictures;
using Application.DTOs.Products;
using AutoMapper;
using Domain.Documents;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories.Pictures;
using Domain.Repositories.Products;
using MediatR;
using SharedKernel;

namespace Application.UseCases.Products.Queries.Handlers
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, Result<PagedResult<ProductResponse>>>
    {
        private readonly IProductsReaderRepository _productsReader;
        private readonly IMongoRepository<ProductImage> _productImageRepository;
        private readonly IMapper _mapper;

        public GetProductsQueryHandler(IProductsReaderRepository productsReader,
            IMapper mapper, IMongoRepository<ProductImage> productImageRepository)
        {
            _productsReader = productsReader;
            _mapper = mapper;
            _productImageRepository = productImageRepository;
        }

        public async Task<Result<PagedResult<ProductResponse>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Step 1: Build Filter Expression
                Expression<Func<Product, bool>>? filter = null;

                // Check if any filter parameters are provided
                if (!string.IsNullOrEmpty(request.SearchTerm) || !string.IsNullOrEmpty(request.Category) || request.MinPrice.HasValue || request.MaxPrice.HasValue)
                {
                    var trimmedSearchTerm = request.SearchTerm?.Trim().ToLower(); // Trim and lower case the search term

                    // Log for debugging
                    Console.WriteLine($"SearchTerm: {request.SearchTerm}, Trimmed: {trimmedSearchTerm}");
                    Console.WriteLine($"Category: {request.Category}, MinPrice: {request.MinPrice}, MaxPrice: {request.MaxPrice}");

                    // Build the filter expression based on provided search parameters
                    filter = p => (string.IsNullOrEmpty(trimmedSearchTerm) || p.Name.ToLower().Contains(trimmedSearchTerm) || p.Description.ToLower().Contains(trimmedSearchTerm))
                                  && (string.IsNullOrEmpty(request.Category) || (p.Category != null && p.Category.Name == request.Category))
                                  && (!request.MinPrice.HasValue || p.Price >= request.MinPrice)
                                  && (!request.MaxPrice.HasValue || p.Price <= request.MaxPrice);

                    // Log the filter expression for debugging
                    Console.WriteLine($"Filter Expression: {filter}");
                }

                // Step 2: Build Sorting Logic
                Func<IQueryable<Product>, IOrderedQueryable<Product>>? orderBy = null;

                if (!string.IsNullOrEmpty(request.SortBy))
                {
                    orderBy = query => request.SortBy.ToLower() switch
                    {
                        "name" => request.SortOrder?.ToLower() == "desc"
                            ? query.OrderByDescending(p => p.Name)
                            : query.OrderBy(p => p.Name),
                        "price" => request.SortOrder?.ToLower() == "desc"
                            ? query.OrderByDescending(p => p.Price)
                            : query.OrderBy(p => p.Price),
                        _ => query.OrderBy(p => p.Name), // Default sorting by name
                    };

                    // Log the sorting logic for debugging
                    Console.WriteLine($"SortBy: {request.SortBy}, SortOrder: {request.SortOrder}");
                }

                // Step 3: Get Data from Repository
                var totalCount = await _productsReader.CountAsync(filter);

                // Log the total count for debugging
                Console.WriteLine($"Total Count: {totalCount}");

                var products = await _productsReader.GetFiltered(filter, orderBy, request.Page, request.PageSize);

                // Log the number of products retrieved for debugging
                Console.WriteLine($"Products Retrieved: {products.Count()}");

                // Step 4: Fetch and Map Images
                var images = await _productImageRepository.GetAll();

                var productResponses = products.Select(product =>
                {
                    // Map the images related to the product
                    var productImages = images.Where(img => img.ProductId == product.Id).ToList();

                    var productResponse = _mapper.Map<ProductResponse>(product);

                    // Map images and attach to the product response
                    productResponse.Images = _mapper.Map<List<PictureResponse>>(productImages);

                    return productResponse;
                }).ToList();

                // Step 5: Return the Paged Result
                return new Result<PagedResult<ProductResponse>>(
                    new PagedResult<ProductResponse>(productResponses, totalCount, request.Page, request.PageSize));
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                Console.WriteLine($"Error in GetProductsQuery Handler: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");

                // Return a failure result
                return new Result<PagedResult<ProductResponse>>(new BusinessLogicException("GetProductsQuery.Failed", ex.Message));
            }
        }

        //public async Task<Result<PagedResult<ProductResponse>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        //{
        //    var totalCount = await _productsReader.CountAsync();
        //    var products = await _productsReader.GetPagedAsync(request.Page, request.PageSize);

        //    var images = await _productImageRepository.GetAll();

        //    // Map images to products using AutoMapper
        //    var productResponses = products.Select(product =>
        //    {
        //        // Filter images by ProductId
        //        var productImages = images.Where(img => img.ProductId == product.Id).ToList();

        //        // Map Product entity to ProductResponse DTO using AutoMapper
        //        var productResponse = _mapper.Map<ProductResponse>(product);

        //        // Attach images to the DTO
        //        productResponse.Images = _mapper.Map<List<PictureResponse>>(productImages);

        //        return productResponse;
        //    }).ToList(); // Ensure it's a List<ProductResponse>

        //    // Return a properly structured Result<PagedResult<ProductResponse>>
        //    return new Result<PagedResult<ProductResponse>>(new PagedResult<ProductResponse>(productResponses, totalCount, request.Page, request.PageSize));
        //}
    }
}
