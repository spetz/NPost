using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NPost.Modules.Deliveries.Core.Repositories;
using NPost.Modules.Parcels.Shared.Events;
using NPost.Shared.Events;

namespace NPost.Modules.Deliveries.Application.Handlers.External
{
    internal class ParcelDeletedHandler : IIntegrationEventHandler<ParcelDeleted>
    {
        private readonly IParcelsRepository _parcelsRepository;
        private readonly ILogger<ParcelDeleted> _logger;

        public ParcelDeletedHandler(IParcelsRepository parcelsRepository, ILogger<ParcelDeleted> logger)
        {
            _parcelsRepository = parcelsRepository;
            _logger = logger;
        }
        
        public async Task HandleAsync(ParcelDeleted @event)
        {
            await _parcelsRepository.DeleteAsync(@event.ParcelId);
            _logger.LogInformation($"Deleted a parcel: {@event.ParcelId:N} from deliveries module.");
        }
    }
}