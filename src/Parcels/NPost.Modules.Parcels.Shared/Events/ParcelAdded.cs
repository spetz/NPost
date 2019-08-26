using System;
using NPost.Shared.Events;

namespace NPost.Modules.Parcels.Shared.Events
{
    public class ParcelAdded : IIntegrationEvent
    {
        public Guid ParcelId { get; }
        public string Name { get; }
        public string Address { get; }

        public ParcelAdded(Guid parcelId, string name, string address)
        {
            ParcelId = parcelId;
            Name = name;
            Address = address;
        }
    }
}