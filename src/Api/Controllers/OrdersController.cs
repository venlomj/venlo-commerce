using Api.Controllers.Base;
using Application.DTOs.Orders;
using Application.UseCases.Invoice.Query;
using Application.UseCases.Orders.Commands;
using Application.UseCases.Orders.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : BaseController
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{id:guid}")]
        public async Task<IActionResult> PlaceOrder([FromRoute] Guid id, [FromBody] OrderRequest request)
        {
            var command = new PlaceOrderCommand(request.Quantity, id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders(
            [FromQuery] string? searchTerm,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate,
            [FromQuery] decimal? minTotalAmount,
            [FromQuery] decimal? maxTotalAmount,
            [FromQuery] string? sortBy,
            [FromQuery] string? sortOrder,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var query = new GetOrdersQuery
            {
                SearchTerm = searchTerm,
                StartDate = startDate,
                EndDate = endDate,
                MinTotalAmount = minTotalAmount,
                MaxTotalAmount = maxTotalAmount,
                SortBy = sortBy,
                SortOrder = sortOrder,
                Page = page,
                PageSize = pageSize
            };

            var result = await _mediator.Send(query);
            // Return the result
            return SendResult(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetOrder([FromRoute] Guid id)
        {
            var query = new GetInvoiceQuery(id);
            var orderResponse = await _mediator.Send(query);

            var documentsPath = Path.Combine(Directory.GetCurrentDirectory(), "Documents");
            var filePath = Path.Combine(documentsPath, $"{orderResponse.OrderNumber}_Invoice.pdf");

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Invoice not found.");
            }

            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);

            return File(new MemoryStream(fileBytes), "application/pdf", $"{orderResponse.OrderNumber}_Invoice.pdf");
        }

    }
}
