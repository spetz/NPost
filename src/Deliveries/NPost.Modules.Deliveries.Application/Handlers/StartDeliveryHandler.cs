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
        private readonly ILogger<StartDeliveryHandler> _logger;

        public StartDeliveryHandler(IDeliveriesRepository deliveriesRepository,
            ILogger<StartDeliveryHandler> logger)
        {
            _deliveriesRepository = deliveriesRepository;
            _logger = logger;
        }
        
        public async Task HandleAsync(StartDelivery command)
        {
            var delivery = new Delivery(command.DeliveryId, command.Parcels,
                new Route(new List<string>(), 100));
            await _deliveriesRepository.AddAsync(delivery);
            _logger.LogInformation($"Started a delivery: {command.DeliveryId:N}");
        }
    }
}