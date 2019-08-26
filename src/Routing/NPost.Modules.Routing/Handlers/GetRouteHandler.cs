using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NPost.Modules.Routing.Shared.DTO;
using NPost.Modules.Routing.Shared.Queries;
using NPost.Shared.Queries;

namespace NPost.Modules.Routing.Handlers
{
    internal class GetRouteHandler : IQueryHandler<GetRoute, RouteDto>
    {
        private readonly IRoutingService _routingService;
        private readonly ILogger<GetRouteHandler> _logger;

        // Inject the different routing strategies.
        public GetRouteHandler(IRoutingService routingService, ILogger<GetRouteHandler> logger)
        {
            _routingService = routingService;
            _logger = logger;
        }

        public async Task<RouteDto> HandleAsync(GetRoute query)
        {
            try
            {
                var addressesInfo = string.Join(", ", query.Addresses);
                _logger.LogInformation($"Calculating the optimal route for addresses: {addressesInfo}...");
                // Let's assume it's a time consuming calculation.
                await Task.Delay(2000);
                var route = _routingService.Calculate(query.Addresses);
                _logger.LogInformation($"The optimal route for addresses: {addressesInfo} was calculated, " +
                                       $"total distance: {route.TotalDistance} km.");

                return new RouteDto
                {
                    Addresses = route.Addresses,
                    TotalDistance = route.TotalDistance
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                
                return null;
            }
        }
    }
}