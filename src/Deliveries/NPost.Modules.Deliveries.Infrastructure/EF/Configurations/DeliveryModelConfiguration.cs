using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NPost.Modules.Deliveries.Infrastructure.EF.Models;

namespace NPost.Modules.Deliveries.Infrastructure.EF.Configurations
{
    internal class DeliveryModelConfiguration : IEntityTypeConfiguration<DeliveryModel>
    {
        public void Configure(EntityTypeBuilder<DeliveryModel> builder)
        {
            builder.Property(p => p.Parcels)
                .HasConversion(p => string.Join(";", p), p => p.Split(';').Select(Guid.Parse));
            builder.OwnsOne<RouteModel>(p => p.Route,
                r => { r.Property(p => p.Addresses).HasConversion(a => string.Join(";", a), a => a.Split(';')); });
        }
    }
}