using System;
using NPost.Modules.Users.Shared.DTO;
using NPost.Shared.Queries;

namespace NPost.Modules.Users.Shared.Queries
{
    public class GetAccount : IQuery<UserDto>
    {
        public Guid UserId { get; }

        public GetAccount(Guid userId)
        {
            UserId = userId;
        }
    }
}