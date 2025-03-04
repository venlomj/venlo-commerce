using Application.DTOs.Roles;
using Application.DTOs.Users;
using AutoMapper;
using Domain.Exceptions;
using Domain.Repositories.Roles;
using Domain.Repositories.Users;
using MediatR;
using SharedKernel;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace Application.UseCases.Users.Queries.Handler
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, Result<UserResponse>>
    {
        private readonly IUserReaderRepository _userReader;
        private readonly IRolesReaderRepository _rolesReader;
        private readonly IMapper _mapper;
        
        public GetUserQueryHandler(IUserReaderRepository userReader, IMapper mapper, IRolesReaderRepository rolesReader)
        {
            _userReader = userReader;
            _mapper = mapper;
            _rolesReader = rolesReader;
        }

        public async Task<Result<UserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userReader.GetById(request.Id);
            if (user == null)
            {
                return new BusinessLogicException($"User with {request.Id} not found.");
            }


            // Alle rollen van de gebruiker ophalen
            var roles = await _rolesReader.GetRolesByUserId(user.Id);

            var roleResponses = _mapper.Map<List<RoleResponse>>(roles);

            var userResponse = _mapper.Map<UserResponse>(user);
            userResponse.Roles = roleResponses;

            return userResponse;
        }
    }
}
