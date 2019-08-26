using System;

namespace NPost.Modules.Parcels.Core.Entities
{
    internal class Parcel
    {
        public Guid Id { get; private set; }
        public Size Size { get; private set; }
        public Status Status { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public string Notes { get; private set; }

        public Parcel(Guid id, Size size, string name, string address, string notes = "")
        {
            Id = id;
            Size = size;
            Address = address;
            Name = string.IsNullOrWhiteSpace(name)
                ? throw new ArgumentException("Missing parcel name.", nameof(name))
                : name;
            Address = string.IsNullOrWhiteSpace(address)
                ? throw new ArgumentException("Missing parcel address.", nameof(address))
                : address;
            Notes = notes ?? string.Empty;
            Status = Status.Available;
        }

        public void SetAvailable() => TrySetStatus(Status.Available, () => Status == Status.InDelivery);

        public void SetInDelivery() => TrySetStatus(Status.InDelivery, () => Status == Status.Available);

        public void SetDelivered() => TrySetStatus(Status.Delivered, () => Status == Status.InDelivery);

        private void TrySetStatus(Status status, Func<bool> validator)
        {
            if (Status == status)
            {
                return;
            }

            if (!validator())
            {
                throw new InvalidOperationException($"Package status cannot be changed from: {Status} to: {status}.");
            }

            Status = status;
        }
    }
}