using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using NPost.Modules.Deliveries.Infrastructure.Services;
using NPost.Modules.Deliveries.Shared.DTO;

namespace NPost.Modules.Deliveries.Infrastructure.Redis
{
    public class DeliveriesDtoStorageRedis : IDeliveriesDtoStorage
    {
        private readonly IDistributedCache _cache;

        public DeliveriesDtoStorageRedis(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<DeliveryDto> GetAsync(Guid id)
        {
            var json = await _cache.GetStringAsync($"deliveries:{id}");

            return string.IsNullOrWhiteSpace(json) ? null : JsonConvert.DeserializeObject<DeliveryDto>(json);
        }

        public async Task SetAsync(DeliveryDto delivery)
        {
            await _cache.SetStringAsync($"deliveries:{delivery.Id}", JsonConvert.SerializeObject(delivery));
        }
    }
}