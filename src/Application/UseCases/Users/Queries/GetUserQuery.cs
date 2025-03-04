using Application.Abstractions.Messaging;
using Application.DTOs.Users;
using SharedKernel;

namespace Application.UseCases.Users.Queries
{
    public class GetUserQuery : IQuery<Result<UserResponse>>
    {
        public Guid Id { get; set; }

        public GetUserQuery(Guid id)
        {
            Id = id;
        }
    }
}
