using Application.Abstractions.Authentication;
using Domain.Repositories.Users;
using Application.Abstractions.Messaging;
using Application.DTOs.Users;
using MediatR;
using SharedKernel;

namespace Application.UseCases.Users.Commands
{
    public class LoginUserCommand : ICommand<Result<string>>
    {
        public LoginUserCommand(LoginRequest loginRequest)
        {
            LoginRequest = loginRequest;
        }

        public LoginRequest LoginRequest { get; set; }
    }
}
