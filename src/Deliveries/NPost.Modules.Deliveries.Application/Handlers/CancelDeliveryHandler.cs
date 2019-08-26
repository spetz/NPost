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
    internal class CancelDeliveryHandler : ICommandHandler<CancelDelivery>
    {
        private readonly IDeliveriesRepository _deliveriesRepository;
        private readonly IDomainEventPublisher _domainEventPublisher;
        private readonly IDispatcher _dispatcher;
        private readonly ILogger<CancelDeliveryHandler> _logger;

        public CancelDeliveryHandler(IDeliveriesRepository deliveriesRepository,
            IDomainEventPublisher domainEventPublisher, IDispatcher dispatcher, ILogger<CancelDeliveryHandler> logger)
        {
            _deliveriesRepository = deliveriesRepository;
            _domainEventPublisher = domainEventPublisher;
            _dispatcher = dispatcher;
            _logger = logger;
        }

        public async Task HandleAsync(CancelDelivery command)
        {
            var delivery = await _deliveriesRepository.GetAsync(command.DeliveryId);
            if (delivery is null)
            {
                throw new Exception($"Delivery: {command.DeliveryId:N} was not found.");
            }

            delivery.Cancel(command.Reason);
            await _deliveriesRepository.UpdateAsync(delivery);
            _logger.LogInformation($"Canceled a delivery: {command.DeliveryId:N}");

            var domainEvents = delivery.Events;
            foreach (var domainEvent in domainEvents)
            {
                await _domainEventPublisher.PublishAsync(domainEvent);
            }

            delivery.ClearEvents();


            var integrationEvents = delivery.Parcels.Select(id =>
                new DeliveryCanceled(command.DeliveryId, id, command.Reason));
            foreach (var integrationEvent in integrationEvents)
            {
                await _dispatcher.PublishAsync(integrationEvent);
            }
        }
    }
}