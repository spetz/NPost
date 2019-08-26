using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NPost.Modules.Deliveries.Core.Entities;
using NPost.Modules.Deliveries.Core.Repositories;

namespace NPost.Modules.Deliveries.Infrastructure.InMemory.Repositories
{
    internal class InMemoryDeliveriesRepository : IDeliveriesRepository
    {
        private readonly ISet<Delivery> _deliveries = new HashSet<Delivery>();

        public Task<bool> HasParcelInDelivery(Guid parcelId)
            => Task.FromResult(_deliveries.Any(d => d.Parcels.Any(p => p == parcelId)));

        public Task<IEnumerable<Delivery>> GetAllAsync() => Task.FromResult(_deliveries.AsEnumerable());

        public Task<Delivery> GetAsync(Guid id) => Task.FromResult(_deliveries.SingleOrDefault(p => p.Id == id));

        public Task AddAsync(Delivery delivery)
        {
            _deliveries.Add(delivery);

            return Task.CompletedTask;
        }

        public Task UpdateAsync(Delivery delivery) => Task.CompletedTask;
    }
}