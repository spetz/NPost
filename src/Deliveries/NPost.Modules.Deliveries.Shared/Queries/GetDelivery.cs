using System;
using NPost.Modules.Deliveries.Shared.DTO;
using NPost.Shared.Queries;

namespace NPost.Modules.Deliveries.Shared.Queries
{
    public class GetDelivery : IQuery<DeliveryDto>
    {
        public Guid DeliveryId { get; }

        public GetDelivery(Guid deliveryId)
        {
            DeliveryId = deliveryId;
        }
    }
}