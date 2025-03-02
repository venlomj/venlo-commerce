using Api.Controllers.Base;
using Application.DTOs.Products;
using Application.DTOs.Users;
using Application.UseCases.Products.Commands;
using Application.UseCases.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRequest request)
        {
            var result = await _mediator.Send(new RegisterUserCommand(
                request.FirstName,
                request.LastName,
                request.Phone,
                request.Email,
                request.PasswordHash)
                );

            return SendResult(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Register([FromBody] LoginRequest request)
        {
            var result = await _mediator.Send(new LoginUserCommand(request));

            return SendResult(result);
        }
    }
}
