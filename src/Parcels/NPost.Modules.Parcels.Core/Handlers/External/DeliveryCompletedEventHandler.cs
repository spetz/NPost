using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NPost.Modules.Deliveries.Shared.Events;
using NPost.Modules.Parcels.Core.Repositories;

namespace NPost.Modules.Parcels.Core.Handlers.External
{
    internal class DeliveryCompletedEventHandler : DeliveryStatusUpdatedEventHandlerBase<DeliveryCompleted>
    {
        private readonly ILogger<DeliveryCompletedEventHandler> _logger;

        public DeliveryCompletedEventHandler(IParcelsRepository parcelsRepository, ILogger<DeliveryCompletedEventHandler> logger)
            : base(parcelsRepository)
        {
            _logger = logger;
        }

        public override async Task HandleAsync(DeliveryCompleted @event)
        {
            await UpdateParcelStatusAsync(@event.ParcelId, p => p.SetDelivered());
            _logger.LogInformation($"Delivery: {@event.DeliveryId:N} has completed, " +
                                   $"updated parcel: {@event.ParcelId:N} status.");
        }
    }
}