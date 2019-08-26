using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NPost.Modules.Parcels.Core.Entities;
using NPost.Modules.Parcels.Core.Repositories;

namespace NPost.Modules.Parcels.Core.InMemory.Repositories
{
    internal class InMemoryParcelsRepository : IParcelsRepository
    {
        private readonly ISet<Parcel> _parcels = new HashSet<Parcel>();

        public Task<IEnumerable<Parcel>> BrowseAsync(Size? size = null, Status? status = null)
        {
            var parcels = _parcels.AsEnumerable();
            if (size.HasValue)
            {
                parcels = parcels.Where(p => p.Size == size);
            }

            if (status.HasValue)
            {
                parcels = parcels.Where(p => p.Status == status);
            }

            return Task.FromResult(parcels);
        }

        public Task<Parcel> GetAsync(Guid id) => Task.FromResult(_parcels.SingleOrDefault(p => p.Id == id));

        public Task AddAsync(Parcel parcel)
        {
            _parcels.Add(parcel);
            
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Parcel parcel) => Task.CompletedTask;

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