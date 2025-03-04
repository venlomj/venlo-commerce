using Application.Abstractions.Authentication;
using Application.DTOs.Users;
using AutoMapper;
using Domain.Exceptions;
using Domain.Repositories.Users;
using Domain.UoW;
using MediatR;
using SharedKernel;

namespace Application.UseCases.Users.Commands.Handler
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<string>>
    {
        private readonly IUserWriterRepository _userWriter;
        private readonly IUserReaderRepository _userReader;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenProvider _tokenProvider;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public LoginUserCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IUserReaderRepository userReader, IUserWriterRepository userWriter, ITokenProvider tokenProvider)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _userReader = userReader;
            _userWriter = userWriter;
            _tokenProvider = tokenProvider;
        }

        public async Task<Result<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userReader.ByEmail(request.LoginRequest.Email);

            if (user is null)
            {
                return new BusinessLogicException($"User with {request.LoginRequest.Email} not found");
            }

            var verified = _passwordHasher.Verify(request.LoginRequest.Password, user.PasswordHash);

            if (!verified)
            {
                return new BusinessLogicException($"User with {request.LoginRequest.Email} not found");
            }

            var token = await _tokenProvider.Create(user);

            return token;
        }
    }
}
