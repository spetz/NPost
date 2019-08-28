using System;

namespace NPost.Modules.Deliveries.Shared.DTO
{
    public class ParcelDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}