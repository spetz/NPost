using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NPost.Modules.Deliveries.Core.Entities;

namespace NPost.Modules.Deliveries.Core.Repositories
{
    internal interface IDeliveriesRepository
    {
        Task<bool> HasParcelInDelivery(Guid parcelId);
        Task<IEnumerable<Delivery>> GetAllAsync();
        Task<Delivery> GetAsync(Guid id);
        Task AddAsync(Delivery delivery);
        Task UpdateAsync(Delivery delivery);
    }
}