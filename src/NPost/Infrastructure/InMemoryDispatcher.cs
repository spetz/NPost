using System.Threading.Tasks;
using NPost.Shared;
using NPost.Shared.Commands;
using NPost.Shared.Events;
using NPost.Shared.Queries;

namespace NPost.Infrastructure
{
    internal sealed class InMemoryDispatcher : IDispatcher
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IIntegrationEventDispatcher _eventDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public InMemoryDispatcher(ICommandDispatcher commandDispatcher,
            IIntegrationEventDispatcher eventDispatcher,
            IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _eventDispatcher = eventDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        public Task SendAsync<T>(T command) where T : class, ICommand
            => _commandDispatcher.SendAsync(command);

        public Task PublishAsync<T>(T @event) where T : class, IIntegrationEvent
            => _eventDispatcher.PublishAsync(@event);

        public Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
            => _queryDispatcher.QueryAsync(query);
    }
}