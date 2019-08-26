using System;
using NPost.Modules.Parcels.Shared.DTO;
using NPost.Shared.Queries;

namespace NPost.Modules.Parcels.Shared.Queries
{
    public class GetParcel : IQuery<ParcelDetailsDto>
    {
        public Guid ParcelId { get; }

        public GetParcel(Guid parcelId)
        {
            ParcelId = parcelId;
        }
    }
}