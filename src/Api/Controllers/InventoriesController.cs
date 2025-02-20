using Api.Controllers.Base;
using Application.UseCases.Inventories.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/inventories")]
    [ApiController]
    public class InventoriesController : BaseController
    {
        private readonly IMediator _mediator;

        public InventoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetInventories([FromQuery] List<string> skuCodes)
        {
            var query = new CheckStockQuery(skuCodes);
            var result = await _mediator.Send(query);
            return SendResult(result);
        }
    }
}
