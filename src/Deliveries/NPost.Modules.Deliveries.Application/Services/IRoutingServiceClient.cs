using System.Collections.Generic;
using System.Threading.Tasks;
using NPost.Modules.Deliveries.Application.DTO;

namespace NPost.Modules.Deliveries.Application.Services
{
    internal interface IRoutingServiceClient
    {
        Task<RouteDto> GetAsync(IEnumerable<string> addresses);
    }
}