using System;
using System.Threading.Tasks;
using NPost.Modules.Users.Core.Entities;

namespace NPost.Modules.Users.Core.Repositories
{
    internal interface IUsersRepository
    {
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(string email);
        Task AddAsync(User user);
    }
}