using System.Threading.Tasks;
using NPost.Modules.Users.Core.Repositories;
using NPost.Modules.Users.Shared.DTO;
using NPost.Modules.Users.Shared.Queries;
using NPost.Shared.Queries;

namespace NPost.Modules.Users.Handlers
{
    internal class GetAccountHandler : IQueryHandler<GetAccount, UserDto>
    {
        private readonly IUsersRepository _usersRepository;

        public GetAccountHandler(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        
        public async Task<UserDto> HandleAsync(GetAccount query)
        {
            var user = await _usersRepository.GetAsync(query.UserId);

            return user is null
                ? null
                : new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Role = user.Role
                };
        }
    }
}