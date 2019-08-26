using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NPost.Modules.Deliveries.Core.Entities;
using NPost.Modules.Deliveries.Core.Repositories;
using NPost.Modules.Parcels.Shared.Events;
using NPost.Shared.Events;

namespace NPost.Modules.Deliveries.Application.Handlers.External
{
    internal class ParcelAddedEventHandler : IIntegrationEventHandler<ParcelAdded>
    {
        private readonly IParcelsRepository _parcelsRepository;
        private readonly ILogger<ParcelAddedEventHandler> _logger;

        public ParcelAddedEventHandler(IParcelsRepository parcelsRepository, ILogger<ParcelAddedEventHandler> logger)
        {
            _parcelsRepository = parcelsRepository;
            _logger = logger;
        }

        public async Task HandleAsync(ParcelAdded @event)
        {
            await _parcelsRepository.AddAsync(new Parcel(@event.ParcelId, @event.Name, @event.Address));
            _logger.LogInformation($"Added a parcel: {@event.ParcelId:N} to deliveries module.");
        }
    }
}