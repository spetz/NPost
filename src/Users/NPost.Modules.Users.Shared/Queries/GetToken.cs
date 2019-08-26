using NPost.Modules.Users.Shared.DTO;
using NPost.Shared.Queries;

namespace NPost.Modules.Users.Shared.Queries
{
    public class GetToken : IQuery<JwtDto>
    {
        public string Email { get; }
        public string Password { get; }

        public GetToken(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}