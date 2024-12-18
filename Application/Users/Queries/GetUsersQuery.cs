using Domain.Entities;
using MediatR;
using Domain.Interfaces;

namespace Application.Users.Queries
{
    public class GetUsersQuery : IRequest<IEnumerable<Domain.Entities.User>>
    {
        public class GetUsersQueryHandler: IRequestHandler<GetUsersQuery, IEnumerable<Domain.Entities.User>> 
        {
            private readonly IUserDapperRepository _UserDapperRepository;

            public GetUsersQueryHandler(IUserDapperRepository UserDapperRepository)
            {
                _UserDapperRepository = UserDapperRepository;
            }

            public async Task<IEnumerable<Domain.Entities.User>> Handle(GetUsersQuery request, CancellationToken cancellation)
            {
                var User = await _UserDapperRepository.GetUsers();
                return User; 
            }
        }
    }
}
