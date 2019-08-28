using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NPost.Modules.Deliveries.Shared.DTO;

namespace NPost.Modules.Deliveries.Infrastructure.Services
{
    internal interface IDeliveriesDtoStorage
    {
        Task<DeliveryDto> GetAsync(Guid id);
        Task SetAsync(DeliveryDto delivery);
    }

    internal class DeliveriesDtoStorage : IDeliveriesDtoStorage
    {
        private readonly ISet<DeliveryDto> _deliveries = new HashSet<DeliveryDto>();

        public Task<DeliveryDto> GetAsync(Guid id)
            => Task.FromResult(_deliveries.SingleOrDefault(d => d.Id == id));

        public async Task SetAsync(DeliveryDto delivery)
        {
            var dto = await GetAsync(delivery.Id);
            if (!(dto is null))
            {
                _deliveries.Remove(dto);
            }

            _deliveries.Add(delivery);
        }
    }
}