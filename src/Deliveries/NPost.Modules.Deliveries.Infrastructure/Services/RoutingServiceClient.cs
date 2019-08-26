using System.Collections.Generic;
using System.Threading.Tasks;
using NPost.Modules.Deliveries.Application.DTO;
using NPost.Modules.Deliveries.Application.Services;
using NPost.Modules.Routing.Shared.Queries;
using NPost.Shared;

namespace NPost.Modules.Deliveries.Infrastructure.Services
{
    internal class RoutingServiceClient : IRoutingServiceClient
    {
        private readonly IDispatcher _dispatcher;

        public RoutingServiceClient(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }
        
        public async Task<RouteDto> GetAsync(IEnumerable<string> addresses)
        {
            var route = await _dispatcher.QueryAsync(new GetRoute
            {
                Addresses = addresses
            });

            return new RouteDto
            {
                Addresses = route.Addresses,
                Distance = route.TotalDistance
            };
        }
    }
}