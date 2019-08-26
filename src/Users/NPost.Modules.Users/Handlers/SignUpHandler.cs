using System;
using System.Threading.Tasks;
using NPost.Modules.Users.Core.Entities;
using NPost.Modules.Users.Core.Repositories;
using NPost.Modules.Users.Services;
using NPost.Modules.Users.Shared.Commands;
using NPost.Shared.Commands;

namespace NPost.Modules.Users.Handlers
{
    internal class SignUpHandler : ICommandHandler<SignUp>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordService _passwordService;

        public SignUpHandler(IUsersRepository usersRepository, IPasswordService passwordService)
        {
            _usersRepository = usersRepository;
            _passwordService = passwordService;
        }

        public async Task HandleAsync(SignUp command)
        {
            var user = await _usersRepository.GetAsync(command.Email);
            if (!(user is null))
            {
                throw new ArgumentException("Email already in use", nameof(command.Email));
            }

            var role = string.IsNullOrWhiteSpace(command.Role) ? "user" : command.Role;
            var hash = _passwordService.Hash(command.Password);
            user = new User(command.Id, command.Email, hash, role);
            await _usersRepository.AddAsync(user);
        }
    }
}