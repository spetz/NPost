using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NPost.Shared.Queries;

namespace NPost.Infrastructure
{
    internal sealed class InMemoryQueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceScopeFactory _serviceFactory;
        private readonly ILogger<InMemoryQueryDispatcher> _logger;

        public InMemoryQueryDispatcher(IServiceScopeFactory serviceFactory, ILogger<InMemoryQueryDispatcher> logger)
        {
            _serviceFactory = serviceFactory;
            _logger = logger;
        }

        public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
        {
            var queryName = query.GetType().Name;
            _logger.LogInformation($"Processing a query: {queryName}");
            using (var scope = _serviceFactory.CreateScope())
            {
                var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
                dynamic handler = scope.ServiceProvider.GetRequiredService(handlerType);
                var handlerName = handler.GetType().Name;
                _logger.LogInformation($"Handling a query: {queryName} by {handlerName}...");
                var result = await handler.HandleAsync((dynamic) query);
                _logger.LogInformation($"Handled a query: {queryName} by {handlerName}.");

                return result;
            }
        }
    }
}