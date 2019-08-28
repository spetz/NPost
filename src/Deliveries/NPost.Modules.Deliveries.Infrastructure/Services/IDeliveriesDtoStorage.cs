using System;
using System.Threading.Tasks;
using NPost.Modules.Deliveries.Shared.DTO;

namespace NPost.Modules.Deliveries.Infrastructure.Services
{
    internal interface IDeliveriesDtoStorage
    {
        Task<DeliveryDto> GetAsync(Guid id);
        Task SetAsync(DeliveryDto delivery);
    }
}