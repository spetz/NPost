using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NPost.Modules.Parcels.Core.Entities;
using NPost.Modules.Parcels.Core.Repositories;
using NPost.Modules.Parcels.Shared.Commands;
using NPost.Modules.Parcels.Shared.Events;
using NPost.Shared;
using NPost.Shared.Commands;

namespace NPost.Modules.Parcels.Core.Handlers
{
    internal class DeleteParcelHandler : ICommandHandler<DeleteParcel>
    {
        private readonly IParcelsRepository _parcelsRepository;
        private readonly IDispatcher _dispatcher;
        private readonly ILogger<DeleteParcelHandler> _logger;

        public DeleteParcelHandler(IParcelsRepository parcelsRepository, IDispatcher dispatcher,
            ILogger<DeleteParcelHandler> logger)
        {
            _parcelsRepository = parcelsRepository;
            _dispatcher = dispatcher;
            _logger = logger;
        }

        public async Task HandleAsync(DeleteParcel command)
        {
            var parcel = await _parcelsRepository.GetAsync(command.ParcelId);
            if (parcel is null)
            {
                throw new Exception($"Parcel: {command.ParcelId:N} was not found.");
            }

            if (parcel.Status != Status.Available)
            {
                throw new Exception($"Parcel: {command.ParcelId:N} cannot be deleted, status: {parcel.Status}.");
            }

            await _parcelsRepository.DeleteAsync(command.ParcelId);
            _logger.LogInformation($"Deleted a parcel: {command.ParcelId:N}.");
            await _dispatcher.PublishAsync(new ParcelDeleted(command.ParcelId));
        }
    }
}