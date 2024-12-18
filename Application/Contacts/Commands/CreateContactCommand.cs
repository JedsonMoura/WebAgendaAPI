using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contacts.Commands
{
    public class CreateContactCommand : ContactCommandBase
    {

        public class CreateContactHandler : IRequestHandler<CreateContactCommand, Contact>
        {
            private readonly IUnitOfWork _unitOfWork;
            public CreateContactHandler(IUnitOfWork unitOfWork, IValidator<CreateContactCommand> validator)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Contact> Handle(CreateContactCommand request, CancellationToken cancellationToken)
            {
                var newContact = new Contact(request.Name, request.Email, request.Phone, request.IsActive);

                await _unitOfWork.ContactRepository.AddContact(newContact);
                await _unitOfWork.CommitAsync();
                return newContact;
            }
        }
    }
}
