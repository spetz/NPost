using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NPost.Modules.Deliveries.Core.Entities;
using NPost.Modules.Deliveries.Core.Repositories;

namespace NPost.Modules.Deliveries.Infrastructure.InMemory.Repositories
{
    internal class InMemoryParcelsRepository : IParcelsRepository
    {
        private readonly ISet<Parcel> _parcels = new HashSet<Parcel>();

        public Task<Parcel> GetAsync(Guid id) => Task.FromResult(_parcels.SingleOrDefault(p => p.Id == id));

        public Task AddAsync(Parcel parcel)
        {
            _parcels.Add(parcel);
            
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var parcel = await GetAsync(id);
            if (parcel is null)
            {
                return;
            }
            
            _parcels.Remove(parcel);
        }
    }
}