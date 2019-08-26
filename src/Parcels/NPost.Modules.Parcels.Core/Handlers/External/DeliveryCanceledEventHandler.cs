using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NPost.Modules.Deliveries.Shared.Events;
using NPost.Modules.Parcels.Core.Repositories;

namespace NPost.Modules.Parcels.Core.Handlers.External
{
    internal class DeliveryCanceledEventHandler : DeliveryStatusUpdatedEventHandlerBase<DeliveryCanceled>
    {
        private readonly ILogger<DeliveryCanceledEventHandler> _logger;

        public DeliveryCanceledEventHandler(IParcelsRepository parcelsRepository, ILogger<DeliveryCanceledEventHandler> logger)
            : base(parcelsRepository)
        {
            _logger = logger;
        }

        public override async Task HandleAsync(DeliveryCanceled @event)
        {
            await UpdateParcelStatusAsync(@event.ParcelId, p => p.SetAvailable());
            _logger.LogInformation($"Delivery: {@event.DeliveryId:N} has been canceled, " +
                                   $"parcel: {@event.ParcelId:N} is available to be delivered once again.");
        }
    }
}