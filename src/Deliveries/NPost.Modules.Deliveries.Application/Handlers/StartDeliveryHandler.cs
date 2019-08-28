using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NPost.Modules.Deliveries.Application.Services;
using NPost.Modules.Deliveries.Core.Entities;
using NPost.Modules.Deliveries.Core.Repositories;
using NPost.Modules.Deliveries.Shared.Commands;
using NPost.Modules.Deliveries.Shared.Events;
using NPost.Shared;
using NPost.Shared.Commands;

namespace NPost.Modules.Deliveries.Application.Handlers
{
    internal class StartDeliveryHandler : ICommandHandler<StartDelivery>
    {
        private readonly IDeliveriesRepository _deliveriesRepository;
        private readonly IParcelsRepository _parcelsRepository;
        private readonly IRoutingServiceClient _routingServiceClient;
        private readonly IDomainEventPublisher _domainEventPublisher;
        private readonly IDispatcher _dispatcher;
        private readonly ILogger<StartDeliveryHandler> _logger;

        public StartDeliveryHandler(IDeliveriesRepository deliveriesRepository,
            IParcelsRepository parcelsRepository, IRoutingServiceClient routingServiceClient,
            IDomainEventPublisher domainEventPublisher, IDispatcher dispatcher,
            ILogger<StartDeliveryHandler> logger)
        {
            _deliveriesRepository = deliveriesRepository;
            _parcelsRepository = parcelsRepository;
            _routingServiceClient = routingServiceClient;
            _domainEventPublisher = domainEventPublisher;
            _dispatcher = dispatcher;
            _logger = logger;
        }
        
        public async Task HandleAsync(StartDelivery command)
        {
            var parcels = new List<Parcel>();
            foreach (var parcelId in command.Parcels)
            {
                var parcel = await _parcelsRepository.GetAsync(parcelId);
                if (parcel is null)
                {
                    throw new  Exception($"Parcel: {parcelId} was not found.");
                }
                
                parcels.Add(parcel);
            }

            var route = await _routingServiceClient.GetAsync(parcels.Select(p => p.Address));
            var delivery = new Delivery(command.DeliveryId, command.Parcels,
                new Route(route.Addresses, route.Distance));
            delivery.Start();
            await _deliveriesRepository.AddAsync(delivery);

            foreach (var domainEvent in delivery.Events)
            {
                await _domainEventPublisher.PublishAsync(domainEvent);
            }
            
            _logger.LogInformation($"Started a delivery: {command.DeliveryId:N}");

            foreach (var parcel in parcels)
            {
                await _dispatcher.PublishAsync(new DeliveryStarted(delivery.Id, parcel.Id));
            }
        }
    }
}