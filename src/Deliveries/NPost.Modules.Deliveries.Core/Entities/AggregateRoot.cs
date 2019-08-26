using System.Collections.Generic;
using System.Linq;

namespace NPost.Modules.Deliveries.Core.Entities
{
    internal abstract class AggregateRoot
    {
        private readonly ISet<IDomainEvent> _events = new HashSet<IDomainEvent>();
        public AggregateId Id { get; protected set; }
        public IEnumerable<IDomainEvent> Events => _events;
        public int Version { get; protected set; }

        protected void AddEvent(IDomainEvent @event)
        {
            if (!_events.Any())
            {
                Version++;
            }

            _events.Add(@event);
        }

        public void ClearEvents() => _events.Clear();
    }
}