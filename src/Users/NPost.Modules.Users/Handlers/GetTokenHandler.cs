using System;
using System.Threading.Tasks;
using NPost.Modules.Users.Core.Repositories;
using NPost.Modules.Users.Services;
using NPost.Modules.Users.Shared.DTO;
using NPost.Modules.Users.Shared.Queries;
using NPost.Shared.Queries;

namespace NPost.Modules.Users.Handlers
{
    internal class GetTokenHandler : IQueryHandler<GetToken, JwtDto>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordService _passwordService;
        private readonly IJwtProvider _jwtProvider;

        public GetTokenHandler(IUsersRepository usersRepository, IPasswordService passwordService,
            IJwtProvider jwtProvider)
        {
            _usersRepository = usersRepository;
            _passwordService = passwordService;
            _jwtProvider = jwtProvider;
        }
        
        public async Task<JwtDto> HandleAsync(GetToken query)
        {
            var user = await _usersRepository.GetAsync(query.Email);
            if (user is null)
            {
                throw new Exception("Invalid credentials.");
            }

            var isPasswordValid = _passwordService.IsValid(user.Password, query.Password);
            if (!isPasswordValid)
            {
                throw new Exception("Invalid credentials.");
            }

            return _jwtProvider.CreateToken(user.Id.ToString("N"), user.Role);
        }
    }
}