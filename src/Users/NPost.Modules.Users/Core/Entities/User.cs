using System;

namespace NPost.Modules.Users.Core.Entities
{
    internal class User
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Role { get; private set; }

        public User(Guid id, string email, string password, string role)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Invalid email", nameof(password));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Invalid password", nameof(password));
            }

            Id = id;
            Email = email.ToLowerInvariant();
            Password = password;
            Role = role.ToLowerInvariant();
        }
    }
}