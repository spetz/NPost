using System.Collections.Generic;
using NPost.Modules.Routing.Shared.DTO;

namespace NPost.Modules.Routing
{
    internal interface IRoutingService
    {
        RouteDto Calculate(IEnumerable<string> addresses);
    }
}