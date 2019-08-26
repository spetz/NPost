using System.Collections.Generic;

namespace NPost.Modules.Deliveries.Application.DTO
{
    internal class RouteDto
    {
        public IEnumerable<string> Addresses { get; set; }
        public double Distance { get; set; }
    }
}