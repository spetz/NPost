using System;
using NPost.Shared.Commands;

namespace NPost.Modules.Parcels.Shared.Commands
{
    public class AddParcel : ICommand
    {
        public Guid ParcelId { get; }
        public string Size { get; }
        public string Name { get; }
        public string Address { get; }

        public AddParcel(Guid parcelId, string size, string name, string address)
        {
            ParcelId = parcelId == Guid.Empty ? Guid.NewGuid() : parcelId;
            Size = size;
            Name = name;
            Address = address;
        }
    }
}