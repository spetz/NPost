using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NPost.Modules.Users.Core.Entities;
using NPost.Modules.Users.Core.Repositories;

namespace NPost.Modules.Users.Infrastructure.InMemory.Repositories
{
    internal class InMemoryUsersRepository : IUsersRepository
    {
        private readonly ISet<User> _users = new HashSet<User>();

        public Task<User> GetAsync(Guid id) => Task.FromResult(_users.SingleOrDefault(u => u.Id == id));

        public Task<User> GetAsync(string email)
            => Task.FromResult(_users.SingleOrDefault(u => 
                u.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase)));

        public Task AddAsync(User user)
        {
            _users.Add(user);
            
            return Task.CompletedTask;
        }
    }
}