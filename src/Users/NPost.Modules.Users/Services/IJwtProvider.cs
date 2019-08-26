using NPost.Modules.Users.Shared.DTO;

namespace NPost.Modules.Users.Services
{
    internal interface IJwtProvider
    {
        JwtDto CreateToken(string userId, string role);
    }
}