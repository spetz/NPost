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
    internal class AddParcelHandler : ICommandHandler<AddParcel>
    {
        private readonly IParcelsRepository _parcelsRepository;
        private readonly IDispatcher _dispatcher;
        private readonly ILogger<AddParcelHandler> _logger;

        public AddParcelHandler(IParcelsRepository parcelsRepository, IDispatcher dispatcher,
            ILogger<AddParcelHandler> logger)
        {
            _parcelsRepository = parcelsRepository;
            _dispatcher = dispatcher;
            _logger = logger;
        }

        public async Task HandleAsync(AddParcel command)
        {
            if (!Enum.TryParse<Size>(command.Size, true, out var size))
            {
                throw new ArgumentException($"Invalid parcel size: {size}.");
            }

            var parcel = new Parcel(command.ParcelId, size, command.Name, command.Address);
            await _parcelsRepository.AddAsync(parcel);
            _logger.LogInformation($"Added a parcel: {command.ParcelId:N}, address: {command.Address}.");
            await _dispatcher.PublishAsync(new ParcelAdded(command.ParcelId, command.Name, command.Address));
        }
    }
}