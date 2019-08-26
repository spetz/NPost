using System.Collections.Generic;

namespace NPost.Modules.Deliveries.Infrastructure.EF.Models
{
    internal class RouteModel
    {
        public IEnumerable<string> Addresses { get; set; }
        public double Distance { get; set; }
    }
}