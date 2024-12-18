using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Contacts.Commands
{
    public class UpdateContactCommand : ContactCommandBase
    {
        public int Id { get; set; }

        public class UpdateContactHandler : IRequestHandler<UpdateContactCommand, Contact>
        {

            private readonly IUnitOfWork _unitOfWork;
            public UpdateContactHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Contact> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
            {
                var existingContact = await _unitOfWork.ContactRepository.GetContactById(request.Id);
                if (existingContact is null)
                    throw new Exception("Contato não encontrado.");

                existingContact.Update(request.Name, request.Email, request.Phone, request.IsActive);

                _unitOfWork.ContactRepository.UpdateContact(existingContact);
                await _unitOfWork.CommitAsync();

                return existingContact;
            }
        }

    }
}
