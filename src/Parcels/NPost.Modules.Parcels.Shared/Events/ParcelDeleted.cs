using System;
using NPost.Shared.Events;

namespace NPost.Modules.Parcels.Shared.Events
{
    public class ParcelDeleted : IIntegrationEvent
    {
        public Guid ParcelId { get; }

        public ParcelDeleted(Guid parcelId)
        {
            ParcelId = parcelId;
        }
    }
}