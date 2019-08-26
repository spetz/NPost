using System.Threading.Tasks;
using NPost.Shared.Commands;
using NPost.Shared.Events;
using NPost.Shared.Queries;

namespace NPost.Shared
{
    public interface IDispatcher
    {
        Task SendAsync<T>(T command) where T : class, ICommand;
        Task PublishAsync<T>(T @event) where T : class, IIntegrationEvent;
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
    }
}