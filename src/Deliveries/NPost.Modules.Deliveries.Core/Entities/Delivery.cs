using System;
using System.Collections.Generic;

namespace NPost.Modules.Deliveries.Core.Entities
{
    internal class Delivery : AggregateRoot
    {
        private ISet<Guid> _parcels = new HashSet<Guid>();
        public IEnumerable<Guid> Parcels
        {
            get => _parcels;
            private set => _parcels = new HashSet<Guid>(value);
        }

        public Route Route { get; private set; }
        public Status Status { get; private set; }
        public string Notes { get; private set; }

        public Delivery(Guid id, IEnumerable<Guid> parcels, Route route, Status status = Status.None)
        {
            Id = id;
            Parcels = parcels ?? throw new ArgumentException("No parcels to be delivered.");
            Route = route ?? throw new ArgumentException("Route cannot be empty.");
            Notes = string.Empty;
            Status = status;
        }

        private void TryChangeStatus(Status status, Func<bool> validator)
        {
            if (!validator())
            {
                throw new InvalidOperationException($"Delivery status cannot be changed to: {status}");
            }

            Status = status;
        }
    }
}