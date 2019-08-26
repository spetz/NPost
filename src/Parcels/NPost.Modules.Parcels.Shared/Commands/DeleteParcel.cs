using System;
using NPost.Shared.Commands;

namespace NPost.Modules.Parcels.Shared.Commands
{
    public class DeleteParcel : ICommand
    {
        public Guid ParcelId { get; }

        public DeleteParcel(Guid parcelId)
        {
            ParcelId = parcelId;
        }
    }
}