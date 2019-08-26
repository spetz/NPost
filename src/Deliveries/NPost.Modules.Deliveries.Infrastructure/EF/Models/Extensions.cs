using NPost.Modules.Deliveries.Core.Entities;

namespace NPost.Modules.Deliveries.Infrastructure.EF.Models
{
    internal static class Extensions
    {
        public static Delivery ToEntity(this DeliveryModel model)
            => new Delivery(model.Id, model.Parcels, model.Route.ToEntity(), model.Status);

        public static Parcel ToEntity(this ParcelModel model)
            => new Parcel(model.Id, model.Name, model.Address);

        public static Route ToEntity(this RouteModel model)
            => new Route(model.Addresses, model.Distance);

        public static DeliveryModel ToModel(this Delivery model)
            => new DeliveryModel
            {
                Id = model.Id,
                Notes = model.Notes,
                Parcels = model.Parcels,
                Route = model.Route.ToModel(),
                Status = model.Status
            };

        public static ParcelModel ToModel(this Parcel model)
            => new ParcelModel
            {
                Id = model.Id,
                Address = model.Address,
                Name = model.Name
            };

        public static RouteModel ToModel(this Route model)
            => new RouteModel
            {
                Addresses = model.Addresses,
                Distance = model.Distance
            };
    }
}