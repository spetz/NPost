using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NPost.Modules.Deliveries.Shared.Queries;
using NPost.Modules.Parcels.Shared.Queries;
using NPost.Shared;

namespace NPost.Modules.Support.Hubs
{
    internal class NPostHub : Hub
    {
        private readonly IServiceScopeFactory _serviceFactory;
        private readonly ILogger<NPostHub> _logger;

        public NPostHub(IServiceScopeFactory serviceFactory, ILogger<NPostHub> logger)
        {
            _serviceFactory = serviceFactory;
            _logger = logger;
        }
        
        public override async Task OnConnectedAsync()
        {
            await Clients.Client(Context.ConnectionId).SendAsync("connected");
        }

        public async Task<string> SendMessage(string message)
        {
            _logger.LogInformation($"Received a message: {message}");
            var parts = message.Split(' ');
            if (parts.Length < 1)
            {
                _logger.LogError($"Invalid message: {message}");
                
                return "Invalid message";
            }

            var command = parts[0].ToLowerInvariant();
            _logger.LogInformation($"Command: {command}");

            switch (command)
            {
                case "deliveries":
                {
                    using (var scope = _serviceFactory.CreateScope())
                    {
                        var dispatcher = scope.ServiceProvider.GetService<IDispatcher>();
                        var deliveries = await dispatcher.QueryAsync(new GetDeliveries());

                        return JsonConvert.SerializeObject(deliveries);
                    }
                }
                
                case "delivery":
                {
                    using (var scope = _serviceFactory.CreateScope())
                    {
                        var deliveryId = parts[1];
                        var dispatcher = scope.ServiceProvider.GetService<IDispatcher>();
                        var delivery = await dispatcher.QueryAsync(new GetDelivery(Guid.Parse(deliveryId)));

                        return JsonConvert.SerializeObject(delivery);
                    }
                }

                case "parcel":
                {
                    using (var scope = _serviceFactory.CreateScope())
                    {
                        var parcelId = parts[1];
                        var dispatcher = scope.ServiceProvider.GetService<IDispatcher>();
                        var parcel = await dispatcher.QueryAsync(new GetParcel(Guid.Parse(parcelId)));

                        return JsonConvert.SerializeObject(parcel);
                    }
                }
                    
                case "parcels":
                {
                    using (var scope = _serviceFactory.CreateScope())
                    {
                        var dispatcher = scope.ServiceProvider.GetService<IDispatcher>();
                        var parcels = await dispatcher.QueryAsync(new GetParcels());

                        return JsonConvert.SerializeObject(parcels);
                    }
                }

                default:
                {
                    return $"Invalid command: {command}";
                }
            }
        }
    }
}