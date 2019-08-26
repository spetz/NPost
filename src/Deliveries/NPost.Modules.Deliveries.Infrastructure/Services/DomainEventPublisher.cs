using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NPost.Modules.Deliveries.Application.Services;
using NPost.Modules.Deliveries.Core;

namespace NPost.Modules.Deliveries.Infrastructure.Services
{
    internal class DomainEventPublisher : IDomainEventPublisher
    {
        private readonly IServiceScopeFactory _serviceFactory;
        private readonly ILogger<DomainEventPublisher> _logger;

        public DomainEventPublisher(IServiceScopeFactory serviceFactory, ILogger<DomainEventPublisher> logger)
        {
            _serviceFactory = serviceFactory;
            _logger = logger;
        }

        public async Task PublishAsync<T>(T @event) where T : class, IDomainEvent
        {
            var eventName = @event.GetType().Name;
            _logger.LogInformation($"Publishing a domain event: {@event.GetType().Name}");
            using (var scope = _serviceFactory.CreateScope())
            {
                var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(@event.GetType());
                dynamic handlers = scope.ServiceProvider.GetServices(handlerType);
                foreach (var handler in handlers)
                {
                    var handlerName = handler.GetType().Name;
                    _logger.LogInformation($"Handling a domain event: {eventName} by {handlerName}...");
                    await handler.HandleAsync((dynamic) @event);
                    _logger.LogInformation($"Handled a domain event: {eventName} by {handlerName}.");
                }
            }
        }
    }
}