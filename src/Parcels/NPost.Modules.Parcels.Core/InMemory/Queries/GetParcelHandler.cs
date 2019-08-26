using System.Threading.Tasks;
using NPost.Modules.Parcels.Core.Repositories;
using NPost.Modules.Parcels.Shared.DTO;
using NPost.Modules.Parcels.Shared.Queries;
using NPost.Shared.Queries;

namespace NPost.Modules.Parcels.Core.InMemory.Queries
{
    internal class GetParcelHandler  : IQueryHandler<GetParcel, ParcelDetailsDto>
    {
        private readonly IParcelsRepository _parcelsRepository;

        public GetParcelHandler(IParcelsRepository parcelsRepository)
        {
            _parcelsRepository = parcelsRepository;
        }
        
        public async Task<ParcelDetailsDto> HandleAsync(GetParcel query)
        {
            var parcel = await _parcelsRepository.GetAsync(query.ParcelId);

            return parcel is null
                ? null
                : new ParcelDetailsDto
                {
                    Id = parcel.Id,
                    Address = parcel.Address,
                    Name = parcel.Name,
                    Notes = parcel.Notes,
                    Size = parcel.Size.ToString(),
                    Status = parcel.Status.ToString()
                };
        }
    }
}