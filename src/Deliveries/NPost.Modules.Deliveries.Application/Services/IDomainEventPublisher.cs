using System.Threading.Tasks;
using NPost.Modules.Deliveries.Core;

namespace NPost.Modules.Deliveries.Application.Services
{
    internal interface IDomainEventPublisher
    {
        Task PublishAsync<T>(T @event) where T : class, IDomainEvent;
    }
}