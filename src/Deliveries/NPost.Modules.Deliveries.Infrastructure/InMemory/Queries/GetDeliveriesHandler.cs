using System.Collections.Generic;
using System.Threading.Tasks;
using NPost.Modules.Deliveries.Infrastructure.Services;
using NPost.Modules.Deliveries.Shared.DTO;
using NPost.Modules.Deliveries.Shared.Queries;
using NPost.Shared.Queries;

namespace NPost.Modules.Deliveries.Infrastructure.InMemory.Queries
{
    internal class GetDeliveriesHandler : IQueryHandler<GetDeliveries, IEnumerable<DeliveryDto>>
    {
        private readonly IDeliveriesDtoStorage _storage;

        public GetDeliveriesHandler(IDeliveriesDtoStorage storage)
        {
            _storage = storage;
        }

        public Task<IEnumerable<DeliveryDto>> HandleAsync(GetDeliveries query)
            => _storage.GetAllAsync();
    }
}