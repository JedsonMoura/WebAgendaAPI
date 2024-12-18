
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Contacts.Queries
{
    public class GetContactByIdQuery : IRequest<Contact>
    {
        public int Id { get; set; }
        public class GetContactByIdQueryHandler : IRequestHandler<GetContactByIdQuery, Contact>
        {
            private readonly IContactDapperRepository _contactDapperRepository;

            public GetContactByIdQueryHandler(IContactDapperRepository contactDapperRepository)
            {
                _contactDapperRepository = contactDapperRepository;
            }

            public async Task<Contact> Handle(GetContactByIdQuery request, CancellationToken cancellation)
            {
                var contact = await _contactDapperRepository.GetContactById(request.Id);
                return contact;
            }
        }
    }
}
