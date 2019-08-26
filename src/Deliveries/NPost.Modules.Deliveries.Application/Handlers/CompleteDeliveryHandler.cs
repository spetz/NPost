using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NPost.Modules.Deliveries.Application.Services;
using NPost.Modules.Deliveries.Core.Repositories;
using NPost.Modules.Deliveries.Shared.Commands;
using NPost.Modules.Deliveries.Shared.Events;
using NPost.Shared;
using NPost.Shared.Commands;

namespace NPost.Modules.Deliveries.Application.Handlers
{
    internal class CompleteDeliveryHandler : ICommandHandler<CompleteDelivery>
    {
        private readonly IDeliveriesRepository _deliveriesRepository;
        private readonly IDomainEventPublisher _domainEventPublisher;
        private readonly IDispatcher _dispatcher;
        private readonly ILogger<CompleteDeliveryHandler> _logger;

        public CompleteDeliveryHandler(IDeliveriesRepository deliveriesRepository,
            IDomainEventPublisher domainEventPublisher, IDispatcher dispatcher, ILogger<CompleteDeliveryHandler> logger)
        {
            _deliveriesRepository = deliveriesRepository;
            _domainEventPublisher = domainEventPublisher;
            _dispatcher = dispatcher;
            _logger = logger;
        }

        public async Task HandleAsync(CompleteDelivery command)
        {
            var delivery = await _deliveriesRepository.GetAsync(command.DeliveryId);
            if (delivery is null)
            {
                throw new Exception($"Delivery: {command.DeliveryId:N} was not found.");
            }

            delivery.Complete();
            await _deliveriesRepository.UpdateAsync(delivery);
            _logger.LogInformation($"Completed a delivery: {command.DeliveryId:N}.");

            var domainEvents = delivery.Events;
            foreach (var domainEvent in domainEvents)
            {
                await _domainEventPublisher.PublishAsync(domainEvent);
            }

            delivery.ClearEvents();

            var integrationEvents = delivery.Parcels.Select(id => new DeliveryCompleted(command.DeliveryId, id));
            foreach (var integrationEvent in integrationEvents)
            {
                await _dispatcher.PublishAsync(integrationEvent);
            }
        }
    }
}