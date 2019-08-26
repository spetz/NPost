using System.Threading.Tasks;

namespace NPost.Shared.Events
{
    public interface IIntegrationEventHandler<in TEvent> where TEvent : class, IIntegrationEvent
    {
        Task HandleAsync(TEvent @event);
    }
}