using System;

namespace NPost.Modules.Deliveries.Infrastructure.EF.Models
{
    internal class ParcelModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}