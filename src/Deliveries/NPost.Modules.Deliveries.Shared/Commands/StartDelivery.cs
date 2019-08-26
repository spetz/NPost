using System;
using System.Collections.Generic;
using NPost.Shared.Commands;

namespace NPost.Modules.Deliveries.Shared.Commands
{
    public class StartDelivery : ICommand
    {
        public Guid DeliveryId { get; }
        public IEnumerable<Guid> Parcels { get; }

        public StartDelivery(Guid deliveryId, IEnumerable<Guid> parcels)
        {
            DeliveryId = deliveryId == Guid.Empty ? Guid.NewGuid() : deliveryId;
            Parcels = parcels;
        }
    }
}