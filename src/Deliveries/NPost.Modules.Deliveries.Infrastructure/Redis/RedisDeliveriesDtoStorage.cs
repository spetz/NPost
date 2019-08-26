using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using NPost.Modules.Deliveries.Infrastructure.Services;
using NPost.Modules.Deliveries.Shared.DTO;

namespace NPost.Modules.Deliveries.Infrastructure.Redis
{
    public class RedisDeliveriesDtoStorage : IDeliveriesDtoStorage
    {
        private const string DeliveriesKey = "deliveries";
        private readonly IDistributedCache _cache;

        public RedisDeliveriesDtoStorage(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<IEnumerable<DeliveryDto>> GetAllAsync()
        {
            var deliveries = await GetDeliveriesAsync();

            return deliveries;
        }

        public async Task<DeliveryDetailsDto> GetAsync(Guid id)
        {
            var delivery = await _cache.GetStringAsync(GetKey(id));

            return string.IsNullOrWhiteSpace(delivery)
                ? null
                : JsonConvert.DeserializeObject<DeliveryDetailsDto>(delivery);
        }

        public async Task SetAsync(DeliveryDetailsDto delivery)
        {
            await _cache.SetStringAsync(GetKey(delivery.Id), JsonConvert.SerializeObject(delivery));

            // This is rather dummy - use a specialized Redis collection type instead (and full StackExchange library).
            var deliveries = await GetDeliveriesAsync();
            deliveries.Add(delivery);
            await _cache.SetStringAsync(DeliveriesKey, JsonConvert.SerializeObject(deliveries));
        }

        private async Task<IList<DeliveryDto>> GetDeliveriesAsync()
        {
            var deliveries = await _cache.GetStringAsync(DeliveriesKey);

            return string.IsNullOrWhiteSpace(deliveries)
                ? new List<DeliveryDto>()
                : JsonConvert.DeserializeObject<IList<DeliveryDto>>(deliveries);
        }

        private string GetKey(Guid id) => $"{DeliveriesKey}:{id:N}";
    }
}