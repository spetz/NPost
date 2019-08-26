using System.Threading.Tasks;

namespace NPost.Shared.Events
{
    public interface IIntegrationEventDispatcher
    {
        Task PublishAsync<T>(T @event) where T : class, IIntegrationEvent;
    }
}