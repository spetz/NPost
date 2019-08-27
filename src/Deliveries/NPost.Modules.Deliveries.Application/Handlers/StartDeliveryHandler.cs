using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NPost.Modules.Deliveries.Core.Entities;
using NPost.Modules.Deliveries.Core.Repositories;
using NPost.Modules.Deliveries.Shared.Commands;
using NPost.Shared.Commands;

namespace NPost.Modules.Deliveries.Application.Handlers
{
    internal class StartDeliveryHandler : ICommandHandler<StartDelivery>
    {
        private readonly IDeliveriesRepository _deliveriesRepository;
        private readonly IParcelsRepository _parcelsRepository;
        private readonly ILogger<StartDeliveryHandler> _logger;

        public StartDeliveryHandler(IDeliveriesRepository deliveriesRepository,
            IParcelsRepository parcelsRepository,
            ILogger<StartDeliveryHandler> logger)
        {
            _deliveriesRepository = deliveriesRepository;
            _parcelsRepository = parcelsRepository;
            _logger = logger;
        }
        
        public async Task HandleAsync(StartDelivery command)
        {
            foreach (var parcelId in command.Parcels)
            {
                var parcel = await _parcelsRepository.GetAsync(parcelId);
                if (parcel is null)
                {
                    throw new  Exception($"Parcel: {parcelId} was not found.");
                }
            }
            
            var delivery = new Delivery(command.DeliveryId, command.Parcels,
                new Route(new List<string>(), 100));
            await _deliveriesRepository.AddAsync(delivery);
            _logger.LogInformation($"Started a delivery: {command.DeliveryId:N}");
        }
    }
}