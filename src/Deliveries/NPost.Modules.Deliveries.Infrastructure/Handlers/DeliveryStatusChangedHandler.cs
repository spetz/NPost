using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NPost.Modules.Deliveries.Application.Services;
using NPost.Modules.Deliveries.Core;
using NPost.Modules.Deliveries.Core.Entities;
using NPost.Modules.Deliveries.Core.Events;
using NPost.Modules.Deliveries.Core.Repositories;
using NPost.Modules.Deliveries.Infrastructure.Services;
using NPost.Modules.Deliveries.Shared.DTO;

namespace NPost.Modules.Deliveries.Infrastructure.Handlers
{
    internal class DeliveryStatusChangedHandler : IDomainEventHandler<DeliveryStatusChanged>
    {
        private readonly IDeliveriesDtoStorage _storage;
        private readonly IParcelsRepository _parcelsRepository;

        public DeliveryStatusChangedHandler(IDeliveriesDtoStorage storage, 
            IParcelsRepository parcelsRepository)
        {
            _storage = storage;
            _parcelsRepository = parcelsRepository;
        }
        
        public async Task HandleAsync(DeliveryStatusChanged @event)
        {
            var dto = Map(@event.Delivery);
            var parcels = new List<ParcelDto>();
            foreach (var parcelDto in dto.Parcels)
            {
                var parcel = await _parcelsRepository.GetAsync(parcelDto.Id);
                parcels.Add(new ParcelDto
                {
                    Id = parcel.Id,
                    Name = parcel.Name,
                    Address = parcel.Address
                });
            }

            dto.Parcels = parcels;
            await _storage.SetAsync(dto);
        }
        
        private static DeliveryDto Map(Delivery delivery)
        {
            return delivery is null
                ? null
                : new DeliveryDto
                {
                    Id = delivery.Id,
                    Parcels = delivery.Parcels.Select(id => new ParcelDto
                    {
                        Id = id
                    }).ToList(),
                    Route = new RouteDto
                    {
                        Addresses = delivery.Route.Addresses,
                        Distance = delivery.Route.Distance
                    },
                    Status = delivery.Status.ToString()
                };
        }
    }
}