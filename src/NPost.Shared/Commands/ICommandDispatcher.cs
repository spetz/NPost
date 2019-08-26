using System.Threading.Tasks;

namespace NPost.Shared.Commands
{
    public interface ICommandDispatcher
    {
        Task SendAsync<T>(T command) where T : class, ICommand;
    }
}