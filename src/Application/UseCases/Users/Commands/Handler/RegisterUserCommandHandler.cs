using Application.Abstractions.Authentication;
using Application.DTOs.Products;
using Application.DTOs.Users;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories.Products;
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
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public RegisterUserCommandHandler(IUserWriterRepository userWriter, IUserReaderRepository userReader, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userWriter = userWriter;
            _userReader = userReader;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var result = await _userReader.GetById(id);

            // Map the saved product back to a response DTO
            return _mapper.Map<Result<UserResponse>>(result);

        }
    }
}
