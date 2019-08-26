using System.Collections.Generic;
using NPost.Modules.Deliveries.Shared.DTO;
using NPost.Shared.Queries;

namespace NPost.Modules.Deliveries.Shared.Queries
{
    public class GetDeliveries : IQuery<IEnumerable<DeliveryDto>>
    {
    }
}