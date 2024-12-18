using Domain.Interfaces;
using MediatR;

namespace Application.Users.Commands
{
    public class DeleteUserCommand : UserCommandBase
    {
        public int Id { get; set; }

        public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, Domain.Entities.User>
        {
            private readonly IUnitOfWork _unitOfWork;

            public DeleteUserHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Domain.Entities.User> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                var deleteUser = await _unitOfWork.UserRepository.DeleteUser(request.Id);

                if (deleteUser is null)
                    throw new Exception("Contact not found!");

                await _unitOfWork.CommitAsync();

                return deleteUser;
            }
        }
    }
}
