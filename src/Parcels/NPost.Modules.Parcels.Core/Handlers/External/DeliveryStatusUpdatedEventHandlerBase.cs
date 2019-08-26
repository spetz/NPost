using System;
using System.Threading.Tasks;
using NPost.Modules.Parcels.Core.Entities;
using NPost.Modules.Parcels.Core.Repositories;
using NPost.Shared.Events;

namespace NPost.Modules.Parcels.Core.Handlers.External
{
    internal abstract class DeliveryStatusUpdatedEventHandlerBase<T> : IIntegrationEventHandler<T> where T : class, IIntegrationEvent
    {
        private readonly IParcelsRepository _parcelsRepository;

        protected DeliveryStatusUpdatedEventHandlerBase(IParcelsRepository parcelsRepository)
        {
            _parcelsRepository = parcelsRepository;
        }

        public abstract Task HandleAsync(T @event);

        protected async Task UpdateParcelStatusAsync(Guid parcelId, Action<Parcel> update)
        {
            var parcel = await _parcelsRepository.GetAsync(parcelId);
            update(parcel);
            await _parcelsRepository.UpdateAsync(parcel);
        }
    }
}