using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NPost.Modules.Deliveries.Core.Entities;
using NPost.Modules.Deliveries.Core.Repositories;
using NPost.Modules.Deliveries.Infrastructure.EF.Models;

namespace NPost.Modules.Deliveries.Infrastructure.EF.Repositories
{
    internal class EfParcelsRepository : IParcelsRepository
    {
        private readonly DeliveriesDbContext _context;

        public EfParcelsRepository(DeliveriesDbContext context)
        {
            _context = context;
        }

        public async Task<Parcel> GetAsync(Guid id)
        {
            var parcel = await GetParcelAsync(id);
            
            return parcel?.ToEntity();
        }

        public async Task AddAsync(Parcel parcel)
        {
            await _context.Parcels.AddAsync(parcel.ToModel());
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var parcel = await GetParcelAsync(id);
            if (parcel is null)
            {
                return;
            }

            _context.Parcels.Remove(parcel);
            await _context.SaveChangesAsync();
        }

        private Task<ParcelModel> GetParcelAsync(Guid id) => _context.Parcels.SingleOrDefaultAsync(p => p.Id == id);
    }
}