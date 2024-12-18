using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Users.Commands
{
    public class UpdateUserCommand : UserCommandBase
    {
        public int Id { get; set; }

        public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Domain.Entities.User>
        {

            private readonly IUnitOfWork _unitOfWork;
            public UpdateUserHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Domain.Entities.User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                var existingUser = await _unitOfWork.UserRepository.GetUserById(request.Id);
                if (existingUser is null)
                    throw new Exception("Contact not found!");

                existingUser.Update(request.Username, request.Password);

                _unitOfWork.UserRepository.UpdateUser(existingUser);
                await _unitOfWork.CommitAsync();

                return existingUser;
            }
        }

    }
}
