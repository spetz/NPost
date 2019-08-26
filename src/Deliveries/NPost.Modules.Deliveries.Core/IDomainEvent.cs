using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("NPost.Modules.Deliveries.Application")]
[assembly: InternalsVisibleTo("NPost.Modules.Deliveries.Infrastructure")]

namespace NPost.Modules.Deliveries.Core
{
    internal interface IDomainEvent
    {
    }
}