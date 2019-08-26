using System;
using NPost.Shared.Commands;

namespace NPost.Modules.Deliveries.Shared.Commands
{
    public class CancelDelivery : ICommand
    {
        public Guid DeliveryId { get; }
        public string Reason { get; }

        public CancelDelivery(Guid deliveryId, string reason)
        {
            DeliveryId = deliveryId;
            Reason = reason;
        }
    }
}