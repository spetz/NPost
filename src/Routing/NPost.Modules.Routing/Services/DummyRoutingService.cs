using System;
using System.Collections.Generic;
using System.Linq;
using NPost.Modules.Routing.Shared.DTO;

namespace NPost.Modules.Routing.Services
{
    internal class DummyRoutingService : IRoutingService
    {
        public RouteDto Calculate(IEnumerable<string> addresses)
        {
            if (addresses is null || !addresses.Any())
            {
                throw new Exception("Route cannot be calculated without the addresses.");
            }
            
            var routeAddresses = addresses.OrderBy(a => a).ToList();
            var totalDistance = routeAddresses.Sum(a => a.Length * 5);

            return new RouteDto
            {
                Addresses = routeAddresses,
                TotalDistance = totalDistance
            };
        }
    }
}