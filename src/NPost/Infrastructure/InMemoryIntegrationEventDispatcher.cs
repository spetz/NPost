using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NPost.Shared.Events;

namespace NPost.Infrastructure
{
    internal sealed class InMemoryIntegrationEventDispatcher : IIntegrationEventDispatcher
    {
        private readonly IServiceScopeFactory _serviceFactory;
        private readonly ILogger<InMemoryIntegrationEventDispatcher> _logger;

        public InMemoryIntegrationEventDispatcher(IServiceScopeFactory serviceFactory,
            ILogger<InMemoryIntegrationEventDispatcher> logger)
        {
            _serviceFactory = serviceFactory;
            _logger = logger;
        }

        public async Task PublishAsync<T>(T @event) where T : class, IIntegrationEvent
        {
            var eventName = @event.GetType().Name;
            _logger.LogInformation($"Publishing an integration event: {eventName}");
            using (var scope = _serviceFactory.CreateScope())
            {
                var handlers = scope.ServiceProvider.GetServices<IIntegrationEventHandler<T>>();
                foreach (var handler in handlers)
                {
                    var handlerName = handler.GetType().Name;
                    _logger.LogInformation($"Handling an integration event: {eventName} by {handlerName}...");
                    await handler.HandleAsync(@event);
                    _logger.LogInformation($"Handled an integration event: {eventName} by {handlerName}.");
                }
            }
        }
    }
}