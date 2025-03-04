using Application.Abstractions.Authentication;
using Application.DTOs.Products;
using Application.DTOs.Roles;
using Application.DTOs.Users;
using AutoMapper;
using Domain.Constants;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories.Products;
using Domain.Repositories.Roles;
using Domain.Repositories.UserRoles;
using Domain.Repositories.Users;
using Domain.UoW;
using MediatR;
using SharedKernel;

namespace Application.UseCases.Users.Commands.Handler
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<UserResponse>>
    {
        private readonly IUserWriterRepository _userWriter;
        private readonly IUserReaderRepository _userReader;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRolesWriterRepository _userRolesWriter;
        private readonly IRolesReaderRepository _rolesReader;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public RegisterUserCommandHandler(IUserWriterRepository userWriter, IUserReaderRepository userReader, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork, IMapper mapper, IUserRolesWriterRepository userRolesWriter, IRolesReaderRepository rolesReader)
        {
            _userWriter = userWriter;
            _userReader = userReader;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRolesWriter = userRolesWriter;
            _rolesReader = rolesReader;
        }

        public async Task<Result<UserResponse>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            bool userExists = await _userReader.UserExists(request.Email);

            if (userExists)
                throw new BusinessLogicException("User already exists");

            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                PasswordHash = _passwordHasher.Hash(request.Password)
            };

            var id = await _userWriter.Add(user);

            var role = await _rolesReader.GetById(RoleConstants.CustomerId);

            var userRole = new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id
            };

            await _userRolesWriter.Add(userRole);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var result = await _userReader.GetById(id);

            // Alle rollen van de gebruiker ophalen
            var roles = await _rolesReader.GetRolesByUserId(result.Id);

            var roleResponses = _mapper.Map<List<RoleResponse>>(roles);

            var response = new UserResponse
            {
                Id = result.Id,
                Email = result.Email,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Phone = result.Phone,
                PasswordHash = result.PasswordHash,
                Roles = roleResponses
            };
            // Map the saved product back to a response DTO
            return _mapper.Map<Result<UserResponse>>(response);

        }
    }
}
