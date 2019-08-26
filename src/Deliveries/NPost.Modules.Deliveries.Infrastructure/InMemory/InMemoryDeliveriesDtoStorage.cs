using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NPost.Modules.Deliveries.Infrastructure.Services;
using NPost.Modules.Deliveries.Shared.DTO;

namespace NPost.Modules.Deliveries.Infrastructure.InMemory
{
    public class InMemoryDeliveriesDtoStorage : IDeliveriesDtoStorage
    {
        private readonly ISet<DeliveryDetailsDto> _deliveries = new HashSet<DeliveryDetailsDto>();

        public async Task<IEnumerable<DeliveryDto>> GetAllAsync()
        {
            await Task.CompletedTask;

            return _deliveries;
        }

        public Task<DeliveryDetailsDto> GetAsync(Guid id)
            => Task.FromResult(_deliveries.SingleOrDefault(p => p.Id == id));

        public Task SetAsync(DeliveryDetailsDto delivery)
        {
            var existingDelivery = _deliveries.SingleOrDefault(d => d.Id == delivery.Id);
            if (!(existingDelivery is null))
            {
                _deliveries.Remove(existingDelivery);
            }

            _deliveries.Add(delivery);

            return Task.CompletedTask;
        }
    }
}