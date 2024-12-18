using Domain.Entities;
using MediatR;
using Domain.Interfaces;

namespace Application.Contacts.Queries
{
    public class GetContactsQuery : IRequest<IEnumerable<Contact>>
    {
        public class GetContactsQueryHandler: IRequestHandler<GetContactsQuery, IEnumerable<Contact>> 
        {
            private readonly IContactDapperRepository _contactDapperRepository;

            public GetContactsQueryHandler(IContactDapperRepository contactDapperRepository)
            {
                _contactDapperRepository = contactDapperRepository;
            }

            public async Task<IEnumerable<Contact>> Handle(GetContactsQuery request, CancellationToken cancellation)
            {
                var contact = await _contactDapperRepository.GetContacts();
                return contact; 
            }
        }
    }
}
