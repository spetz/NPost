using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NPost.Shared.Commands;

namespace NPost.Infrastructure
{
    internal sealed class InMemoryCommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceScopeFactory _serviceFactory;
        private readonly ILogger<InMemoryCommandDispatcher> _logger;

        public InMemoryCommandDispatcher(IServiceScopeFactory serviceFactory, ILogger<InMemoryCommandDispatcher> logger)
        {
            _serviceFactory = serviceFactory;
            _logger = logger;
        }

        public async Task SendAsync<T>(T command) where T : class, ICommand
        {
            var commandName = command.GetType().Name;
            _logger.LogInformation($"Sending a command: {commandName}");
            using (var scope = _serviceFactory.CreateScope())
            {
                var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<T>>();
                var handlerName = handler.GetType().Name;
                _logger.LogInformation($"Handling a command: {commandName} by {handlerName}...");
                await handler.HandleAsync(command);
                _logger.LogInformation($"Handled a command: {commandName} by {handlerName}.");
            }
        }
    }
}