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

        public StartDeliveryHandler(IDeliveriesRepository deliveriesRepository, IParcelsRepository parcelsRepository,
            IRoutingServiceClient routingServiceClient, IDomainEventPublisher domainEventPublisher,
            IDispatcher dispatcher, ILogger<StartDeliveryHandler> logger)
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
            if (command.Parcels is null)
            {
                throw new Exception("Parcels to be delivered were not specified.");
            }

            var parcels = new HashSet<Parcel>();
            foreach (var parcelId in command.Parcels)
            {
                var parcel = await _parcelsRepository.GetAsync(parcelId);
                if (parcel is null)
                {
                    throw new Exception($"Parcel: {parcelId:N} to be delivered was not found.");
                }

                if (await _deliveriesRepository.HasParcelInDelivery(parcelId))
                {
                    throw new Exception($"Parcel: {parcelId:N} is unavailable.");
                }

                parcels.Add(parcel);
            }

            if (!parcels.Any())
            {
                throw new Exception($"Delivery cannot be started without the parcels.");
            }

            _logger.LogInformation("Calculating the route...");
            var route = await _routingServiceClient.GetAsync(parcels.Select(p => p.Address));
            if (route is null)
            {
                throw new Exception("Route was not found.");
            }

            _logger.LogInformation("Route was calculated.");
            var delivery = new Delivery(command.DeliveryId, parcels.Select(p => p.Id),
                new Route(route.Addresses, route.Distance));
            delivery.Start();
            await _deliveriesRepository.AddAsync(delivery);
            _logger.LogInformation($"Started a delivery: {command.DeliveryId:N}");

            var domainEvents = delivery.Events;
            foreach (var domainEvent in domainEvents)
            {
                await _domainEventPublisher.PublishAsync(domainEvent);
            }

            delivery.ClearEvents();

            var events = parcels.Select(p => new DeliveryStarted(command.DeliveryId, p.Id));
            foreach (var @event in events)
            {
                await _dispatcher.PublishAsync(@event);
            }
        }
    }
}