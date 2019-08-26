using System;
using NPost.Shared.Commands;

namespace NPost.Modules.Deliveries.Shared.Commands
{
    public class CompleteDelivery : ICommand
    {
        public Guid DeliveryId { get; }

        public CompleteDelivery(Guid deliveryId)
        {
            DeliveryId = deliveryId;
        }
    }
}