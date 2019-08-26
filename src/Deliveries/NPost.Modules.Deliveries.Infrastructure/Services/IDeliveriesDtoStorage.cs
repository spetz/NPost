using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NPost.Modules.Deliveries.Shared.DTO;

namespace NPost.Modules.Deliveries.Infrastructure.Services
{
    internal interface IDeliveriesDtoStorage
    {
        Task<IEnumerable<DeliveryDto>> GetAllAsync();
        Task<DeliveryDetailsDto> GetAsync(Guid id);
        Task SetAsync(DeliveryDetailsDto delivery);
    }
}