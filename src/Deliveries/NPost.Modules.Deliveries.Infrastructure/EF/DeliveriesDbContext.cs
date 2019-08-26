using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NPost.Modules.Deliveries.Infrastructure.EF.Configurations;
using NPost.Modules.Deliveries.Infrastructure.EF.Models;
using NPost.Shared.Options;

namespace NPost.Modules.Deliveries.Infrastructure.EF
{
    internal class DeliveriesDbContext : DbContext
    {
        private readonly IOptions<EfOptions> _options;
        
        public DbSet<DeliveryModel> Deliveries { get; set; }
        public DbSet<ParcelModel> Parcels { get; set; }

        public DeliveriesDbContext(DbContextOptions<DeliveriesDbContext> dbContextOptions, IOptions<EfOptions> options)
            : base(dbContextOptions)
        {
            _options = options;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }

            if (_options.Value.InMemory)
            {
                optionsBuilder.UseInMemoryDatabase("Deliveries");
                return;
            }

            optionsBuilder.UseSqlServer(_options.Value.ConnectionString,
                o => { o.MigrationsAssembly("NPost.Modules.Deliveries.Infrastructure"); });
        }
        
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new DeliveryModelConfiguration());
        }
    }
}