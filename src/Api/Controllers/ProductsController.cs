using Api.Controllers.Base;
using Application.DTOs.Pictures;
using Application.DTOs.Products;
using Application.UseCases.Pictures.Command;
using Application.UseCases.Products.Commands;
using Application.UseCases.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController(IMediator mediator) : BaseController
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Retrieves a paginated and filtered list of products.
        /// </summary>
        /// <param name="page">The page number.</param>
        /// <param name="pageSize">The number of products per page.</param>
        /// <param name="searchTerm">Search term to filter products by name or description.</param>
        /// <param name="category">Category to filter products by.</param>
        /// <param name="minPrice">Minimum price to filter products by.</param>
        /// <param name="maxPrice">Maximum price to filter products by.</param>
        /// <param name="sortBy">Field to sort by (e.g., "name" or "price").</param>
        /// <param name="sortOrder">Order for sorting ("asc" or "desc").</param>
        /// <returns>A paginated list of products.</returns>
        [HttpGet]
        public async Task<IActionResult> GetProducts(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null,
            [FromQuery] string? category = null,
            [FromQuery] decimal? minPrice = null,
            [FromQuery] decimal? maxPrice = null,
            [FromQuery] string? sortBy = null,
            [FromQuery] string? sortOrder = null)
        {
            // Send query with the new parameters
            var result = await _mediator.Send(new GetProductsQuery(page, pageSize, searchTerm, category, minPrice, maxPrice, sortBy, sortOrder));

            // Return the result
            return SendResult(result);
        }



        [HttpGet("category/{categoryId:int}")]
        public async Task<IActionResult> GetProductsByCategory([FromRoute] int categoryId)
        {
            var result = await _mediator.Send(new GetProductsByCategoryQuery(categoryId));
            return SendResult(result);
        }


        /// <summary>
        /// Adds a new product.
        /// </summary>
        /// <param name="request">The product to add.</param>
        /// <returns>The added product.</returns>
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductRequest request)
        {
            var result = await _mediator.Send(new AddProductCommand(request));

            return SendResult(result);
        }

        /// <summary>
        /// Retrieves a specific product by unique id.
        /// </summary>
        /// <param name="id">The id of the product to retrieve.</param>
        /// <returns>The product with the specified id.</returns>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var result = await _mediator.Send(new GetProductQuery(id));

            return SendResult(result);
        }
        /// <summary>
        /// Retrieves products by a list of SKU codes.
        /// </summary>
        /// <param name="skuCodes">The list of SKU codes to search for products.</param>
        /// <returns>A list of products matching the SKU codes.</returns>
        [HttpGet("sku")]
        public async Task<IActionResult> GetProductsBySkuCode([FromQuery] List<string> skuCodes)
        {
            var query = new GetProductsBySkuCode(skuCodes);
            var result = await _mediator.Send(query);

            return SendResult(result);
        }

        /// <summary>
        /// Updates a specific product by unique id.
        /// </summary>
        /// <param name="id">The id of the product to update.</param>
        /// <param name="request">The updated product information.</param>
        /// <returns>The updated product.</returns>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductRequest request)
        {
            var result = await _mediator.Send(new UpdateProductCommand(id, request));

            return SendResult(result);
        }

        /// <summary>
        /// Deletes a specific product by unique id.
        /// </summary>
        /// <param name="id">The id of the product to delete.</param>
        /// <returns>A confirmation message if the deletion is successful.</returns>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var result = await _mediator.Send(new DeleteProductCommand(id));

            return SendResult(result);
        }
    }
}
