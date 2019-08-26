using System;

namespace NPost.Modules.Parcels.Shared.DTO
{
    public class ParcelDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public string Status { get; set; }
    }
}