using Domain.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Commands
{
    public class CreateUserCommand : UserCommandBase
    {

        public class CreateUserHandler : IRequestHandler<CreateUserCommand, Domain.Entities.User>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IPasswordHasher<Domain.Entities.User> _passwordHasher;
            public CreateUserHandler(IUnitOfWork unitOfWork, IPasswordHasher<Domain.Entities.User> passwordHasher)
            {
                _unitOfWork = unitOfWork;
                _passwordHasher = passwordHasher;

            }

            public async Task<Domain.Entities.User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {

                var newUser = new Domain.Entities.User(request.Username, request.Password);

                var hashedPassword = _passwordHasher.HashPassword(newUser, request.Password);

                newUser.SetPasswordHash(hashedPassword);

                await _unitOfWork.UserRepository.AddUser(newUser);
                await _unitOfWork.CommitAsync();
                return newUser;
            }
        }
    }
}
