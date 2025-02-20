using Api.Controllers.Base;
using Application.DTOs.Orders;
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
        public async Task<IActionResult> GetOrders()
        {
            var result = await _mediator.Send(new GetOrdersQuery());
            return Ok(result);
        }


    }
}
