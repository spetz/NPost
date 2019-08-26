using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NPost.Modules.Deliveries.Application.Services;
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
        private readonly ILogger<DeliveryStatusChangedHandler> _logger;

        public DeliveryStatusChangedHandler(IDeliveriesDtoStorage storage, IParcelsRepository parcelsRepository,
            ILogger<DeliveryStatusChangedHandler> logger)
        {
            _storage = storage;
            _parcelsRepository = parcelsRepository;
            _logger = logger;
        }

        public async Task HandleAsync(DeliveryStatusChanged @event)
        {
            var deliveryDto = Map(@event.Delivery);
            var parcels = new List<ParcelInDeliveryDto>();
            foreach (var parcelDto in deliveryDto.Parcels)
            {
                var parcel = await _parcelsRepository.GetAsync(parcelDto.Id);
                parcels.Add(new ParcelInDeliveryDto
                {
                    Id = parcel.Id,
                    Name = parcel.Name,
                    Address = parcel.Address
                });
            }

            deliveryDto.Parcels = parcels;

            await _storage.SetAsync(deliveryDto);
            _logger.LogInformation($"Set DTO for delivery: {@event.Delivery.Id} in storage.");
        }

        private static DeliveryDetailsDto Map(Delivery delivery)
        {
            return delivery is null
                ? null
                : new DeliveryDetailsDto
                {
                    Id = delivery.Id,
                    Notes = delivery.Notes,
                    Parcels = delivery.Parcels.Select(id => new ParcelInDeliveryDto
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