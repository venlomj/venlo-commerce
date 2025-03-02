using Application.Abstractions.Messaging;
using Application.DTOs.Users;
using SharedKernel;

namespace Application.UseCases.Users.Commands
{
    public record RegisterUserCommand(string FirstName, string LastName, string Phone, string Email, string Password)
        : ICommand<Result<UserResponse>>;

}
