using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NPost.Modules.Parcels.Core.Entities;
using NPost.Modules.Parcels.Core.Repositories;
using NPost.Modules.Parcels.Shared.DTO;
using NPost.Modules.Parcels.Shared.Queries;
using NPost.Shared.Queries;

namespace NPost.Modules.Parcels.Core.InMemory.Queries
{
    internal class GetParcelsHandler : IQueryHandler<GetParcels, IEnumerable<ParcelDto>>
    {
        private readonly IParcelsRepository _parcelsRepository;

        public GetParcelsHandler(IParcelsRepository parcelsRepository)
        {
            _parcelsRepository = parcelsRepository;
        }

        public async Task<IEnumerable<ParcelDto>> HandleAsync(GetParcels query)
        {
            Size? size = null;
            Status? status = null;
            if (Enum.TryParse<Size>(query.Size, true, out var parcelSize))
            {
                size = parcelSize;
            }

            if (Enum.TryParse<Status>(query.Status, true, out var parcelStatus))
            {
                status = parcelStatus;
            }

            var parcels = await _parcelsRepository.BrowseAsync(size, status);

            return parcels.Select(p => new ParcelDto
            {
                Id = p.Id,
                Name = p.Name,
                Size = p.Size.ToString(),
                Status = p.Status.ToString()
            });
        }
    }
}