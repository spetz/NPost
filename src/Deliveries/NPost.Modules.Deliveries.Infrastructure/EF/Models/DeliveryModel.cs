using System;
using System.Collections.Generic;
using NPost.Modules.Deliveries.Core.Entities;

namespace NPost.Modules.Deliveries.Infrastructure.EF.Models
{
    internal class DeliveryModel
    {
        public Guid Id { get; set; }
        public IEnumerable<Guid> Parcels { get; set; }
        public RouteModel Route { get; set; }
        public Status Status { get; set; }
        public string Notes { get; set; }
    }
}