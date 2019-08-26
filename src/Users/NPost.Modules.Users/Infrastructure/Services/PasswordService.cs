using Microsoft.AspNetCore.Identity;
using NPost.Modules.Users.Services;

namespace NPost.Modules.Users.Infrastructure.Services
{
    internal class PasswordService : IPasswordService
    {
        private readonly IPasswordHasher<IPasswordService> _passwordHasher;

        public PasswordService(IPasswordHasher<IPasswordService> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public string Hash(string password)
            => _passwordHasher.HashPassword(this, password);

        public bool IsValid(string hash, string password)
            => _passwordHasher.VerifyHashedPassword(this, hash, password)
               != PasswordVerificationResult.Failed;
    }
}