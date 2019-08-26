using System;
using NPost.Shared.Events;

namespace NPost.Modules.Deliveries.Shared.Events
{
    public class DeliveryCompleted : IIntegrationEvent
    {
        public Guid DeliveryId { get; }
        public Guid ParcelId { get; }

        public DeliveryCompleted(Guid deliveryId, Guid parcelId)
        {
            DeliveryId = deliveryId;
            ParcelId = parcelId;
        }
    }
}