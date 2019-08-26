using System;
using System.Threading.Tasks;
using NPost.Modules.Deliveries.Core.Entities;

namespace NPost.Modules.Deliveries.Core.Repositories
{
    internal interface IParcelsRepository
    {
        Task<Parcel> GetAsync(Guid id);
        Task AddAsync(Parcel parcel);
        Task DeleteAsync(Guid id);
    }
}