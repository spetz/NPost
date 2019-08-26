using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NPost.Modules.Deliveries.Shared.Events;
using NPost.Modules.Parcels.Core.Repositories;

namespace NPost.Modules.Parcels.Core.Handlers.External
{
    internal class DeliveryStartedEventHandler : DeliveryStatusUpdatedEventHandlerBase<DeliveryStarted>
    {
        private readonly ILogger<DeliveryStartedEventHandler> _logger;

        public DeliveryStartedEventHandler(IParcelsRepository parcelsRepository, ILogger<DeliveryStartedEventHandler> logger)
            : base(parcelsRepository)
        {
            _logger = logger;
        }

        public override async Task HandleAsync(DeliveryStarted @event)
        {
            await UpdateParcelStatusAsync(@event.ParcelId, p => p.SetInDelivery());
            _logger.LogInformation($"Delivery: {@event.DeliveryId:N} has started, " +
                                   $"updated parcel: {@event.ParcelId:N} status.");
        }
    }
}