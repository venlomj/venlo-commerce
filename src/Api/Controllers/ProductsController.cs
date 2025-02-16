using Application.DTOs.Products;
using Application.UseCases.Products.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Api.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var result = await _mediator.Send(new GetProductsQuery());
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var result = await _mediator.Send(new GetProductQuery(id));

            return result.Match<IActionResult>(
                onSuccess: result => Ok(result),
                onFailure: ex => BadRequest(new { message = ex.Message }),
                onNull: () => NotFound(new { message = "Product not found." })
            );
        }

        //[HttpGet("{id:guid}")]
        //public async Task<IActionResult> GetProduct(Guid id)
        //{
        //    var result = await _mediator.Send(new GetProductQuery(id));

        //    if (result.IsSuccess)
        //    {
        //        return Ok(result);
        //    }

        //    return NotFound(result.Exception?.Message);
        //}
        //[HttpGet]
        //[Route("{id:guid}")]
        //public async Task<IActionResult> GetProduct([FromRoute] Guid id)
        //{
        //    var result = await _mediator.Send(new GetProductQuery(id));

        //    return Ok(result);
        //}
    }
}
