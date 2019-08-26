using System.Collections.Generic;
using NPost.Modules.Routing.Shared.DTO;
using NPost.Shared.Queries;

namespace NPost.Modules.Routing.Shared.Queries
{
    public class GetRoute : IQuery<RouteDto>
    {
        public IEnumerable<string> Addresses { get; set; }
    }
}