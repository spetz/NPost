using System.Collections.Generic;

namespace NPost.Modules.Routing.Shared.DTO
{
    public class RouteDto
    {
        public IEnumerable<string> Addresses { get; set; }
        public double TotalDistance { get; set; }
    }
}