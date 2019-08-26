using System.Threading.Tasks;
using NPost.Modules.Deliveries.Infrastructure.Services;
using NPost.Modules.Deliveries.Shared.DTO;
using NPost.Modules.Deliveries.Shared.Queries;
using NPost.Shared.Queries;

namespace NPost.Modules.Deliveries.Infrastructure.InMemory.Queries
{
    internal class GetDeliveryHandler : IQueryHandler<GetDelivery, DeliveryDetailsDto>
    {
        private readonly IDeliveriesDtoStorage _storage;

        public GetDeliveryHandler(IDeliveriesDtoStorage storage)
        {
            _storage = storage;
        }

        public Task<DeliveryDetailsDto> HandleAsync(GetDelivery query)
            => _storage.GetAsync(query.DeliveryId);
    }
}