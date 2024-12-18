using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Contacts.Commands
{
    public class DeleteContacttCommand : ContactCommandBase
    {
        public int Id { get; set; }

        public class DeleteContactHandler : IRequestHandler<DeleteContacttCommand, Contact>
        {
            private readonly IUnitOfWork _unitOfWork;

            public DeleteContactHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Contact> Handle(DeleteContacttCommand request, CancellationToken cancellationToken)
            {
                var deleteContact = await _unitOfWork.ContactRepository.DeleteContact(request.Id);

                if (deleteContact is null)
                    throw new Exception("Contato não encontrado.");

                await _unitOfWork.CommitAsync();

                return deleteContact;
            }
        }
    }
}
