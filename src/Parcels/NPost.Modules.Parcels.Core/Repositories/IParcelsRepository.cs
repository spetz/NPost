using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NPost.Modules.Parcels.Core.Entities;

namespace NPost.Modules.Parcels.Core.Repositories
{
    internal interface IParcelsRepository
    {
        Task<IEnumerable<Parcel>> BrowseAsync(Size? size = null, Status? status = null);
        Task<Parcel> GetAsync(Guid id);
        Task AddAsync(Parcel parcel);
        Task UpdateAsync(Parcel parcel);
        Task DeleteAsync(Guid id);
    }
}