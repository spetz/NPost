using System.Collections.Generic;
using NPost.Modules.Parcels.Shared.DTO;
using NPost.Shared.Queries;

namespace NPost.Modules.Parcels.Shared.Queries
{
    public class GetParcels : IQuery<IEnumerable<ParcelDto>>
    {
        public string Size { get; set; }
        public string Status { get; set; }
    }
}