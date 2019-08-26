using System;
using NPost.Shared.Events;

namespace NPost.Modules.Deliveries.Shared.Events
{
    public class DeliveryStarted : IIntegrationEvent
    {
        public Guid DeliveryId { get; }
        public Guid ParcelId { get; }

        public DeliveryStarted(Guid deliveryId, Guid parcelId)
        {
            DeliveryId = deliveryId;
            ParcelId = parcelId;
        }
    }
}