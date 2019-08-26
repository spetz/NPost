using System.Threading.Tasks;
using NPost.Modules.Deliveries.Core;

namespace NPost.Modules.Deliveries.Application.Services
{
    internal interface IDomainEventHandler<in TEvent> where TEvent : class, IDomainEvent
    {
        Task HandleAsync(TEvent @event);
    }
}