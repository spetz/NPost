using System.Collections.Generic;

namespace NPost.Modules.Deliveries.Shared.DTO
{
    public class RouteDto
    {
        public IEnumerable<string> Addresses { get; set; }
        public double Distance { get; set; }
    }
}