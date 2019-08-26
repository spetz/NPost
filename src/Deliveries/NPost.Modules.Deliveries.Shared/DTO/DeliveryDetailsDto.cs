using System.Collections.Generic;

namespace NPost.Modules.Deliveries.Shared.DTO
{
    public class DeliveryDetailsDto : DeliveryDto
    {
        public IEnumerable<ParcelInDeliveryDto> Parcels { get; set; }
        public RouteDto Route { get; set; }
        public string Notes { get; set; }
    }
}