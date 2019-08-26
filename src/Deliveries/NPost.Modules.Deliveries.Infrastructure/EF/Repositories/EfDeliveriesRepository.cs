using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NPost.Modules.Deliveries.Core.Entities;
using NPost.Modules.Deliveries.Core.Repositories;
using NPost.Modules.Deliveries.Infrastructure.EF.Models;

namespace NPost.Modules.Deliveries.Infrastructure.EF.Repositories
{
    internal class EfDeliveriesRepository : IDeliveriesRepository
    {
        private readonly DeliveriesDbContext _context;

        public EfDeliveriesRepository(DeliveriesDbContext context)
        {
            _context = context;
        }

        public Task<bool> HasParcelInDelivery(Guid parcelId)
            => Task.FromResult(_context.Deliveries.Any(d => d.Parcels.Any(p => p == parcelId)));

        public async Task<IEnumerable<Delivery>> GetAllAsync()
        {
            var deliveries = await _context.Deliveries.ToListAsync();

            return deliveries.Select(d => d.ToEntity());
        }

        public async Task<Delivery> GetAsync(Guid id)
        {
            var delivery = await _context.Deliveries.SingleOrDefaultAsync(d => d.Id == id);

            return delivery?.ToEntity();
        }

        public async Task AddAsync(Delivery delivery)
        {
            await _context.Deliveries.AddAsync(delivery.ToModel());
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Delivery delivery)
        {
            var model = _context.Set<DeliveryModel>().Local.SingleOrDefault(entry => entry.Id.Equals(delivery.Id));
            if (model is null)
            {
                return;
            }

            model.Parcels = delivery.Parcels;
            model.Status = delivery.Status;
            model.Notes = delivery.Notes;
            model.Route = delivery.Route.ToModel();
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}