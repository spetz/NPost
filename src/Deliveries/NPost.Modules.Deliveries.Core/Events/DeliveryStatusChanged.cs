using NPost.Modules.Deliveries.Core.Entities;

namespace NPost.Modules.Deliveries.Core.Events
{
    internal class DeliveryStatusChanged : IDomainEvent
    {
        public Delivery Delivery { get; }

        public DeliveryStatusChanged(Delivery delivery)
        {
            Delivery = delivery;
        }
    }
}