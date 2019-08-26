using System;

namespace NPost.Modules.Deliveries.Core.Entities
{
    internal class Parcel
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }

        public Parcel(Guid id, string name, string address)
        {
            Id = id;
            Name = name;
            Address = address;
        }
    }
}