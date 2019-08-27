using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NPost.Modules.Deliveries.Shared.Commands;
using NPost.Shared.Commands;

namespace NPost.Modules.Deliveries.Application.Handlers
{
    public class StartDeliveryHandler : ICommandHandler<StartDelivery>
    {
        private readonly ILogger<StartDeliveryHandler> _logger;

        public StartDeliveryHandler(ILogger<StartDeliveryHandler> logger)
        {
            _logger = logger;
        }
        
        public async Task HandleAsync(StartDelivery command)
        {
            await Task.CompletedTask;
            _logger.LogInformation($"Started a delivery: {command.DeliveryId:N}");
        }
    }
}