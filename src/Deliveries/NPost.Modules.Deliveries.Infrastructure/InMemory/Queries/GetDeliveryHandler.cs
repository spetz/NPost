using System.Threading.Tasks;
using NPost.Modules.Deliveries.Core.Repositories;
using NPost.Modules.Deliveries.Shared.DTO;
using NPost.Modules.Deliveries.Shared.Queries;
using NPost.Shared.Queries;

namespace NPost.Modules.Deliveries.Infrastructure.InMemory.Queries
{
    internal class GetDeliveryHandler : IQueryHandler<GetDelivery, DeliveryDto>
    {
        private readonly IDeliveriesRepository _deliveriesRepository;

        public GetDeliveryHandler(IDeliveriesRepository deliveriesRepository)
        {
            _deliveriesRepository = deliveriesRepository;
        }
        
        public async Task<DeliveryDto> HandleAsync(GetDelivery query)
        {
            var delivery = await _deliveriesRepository.GetAsync(query.DeliveryId);

            return delivery is null
                ? null
                : new DeliveryDto
                {
                    Id = delivery.Id,
                    Status = delivery.Status.ToString()
                };
        }
    }
}