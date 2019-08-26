using System;
using NPost.Shared.Events;

namespace NPost.Modules.Deliveries.Shared.Events
{
    public class DeliveryCanceled : IIntegrationEvent
    {
        public Guid DeliveryId { get; }
        public Guid ParcelId { get; }
        public string Reason { get; }

        public DeliveryCanceled(Guid deliveryId, Guid parcelId, string reason)
        {
            DeliveryId = deliveryId;
            ParcelId = parcelId;
            Reason = reason;
        }
    }
}