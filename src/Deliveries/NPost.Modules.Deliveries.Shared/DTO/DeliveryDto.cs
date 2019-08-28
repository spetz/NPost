using System;
using System.Collections.Generic;

namespace NPost.Modules.Deliveries.Shared.DTO
{
    public class DeliveryDto
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public IEnumerable<ParcelDto> Parcels { get; set; }
        public RouteDto Route { get; set; }
    }
}