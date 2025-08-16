using Microsoft.Extensions.DependencyInjection;

namespace Util.Interfaces
{
    public interface IScopedFactory
    {
        T GetScopedService<T>() where T : class;
        IServiceScope CreateScope();
    }
}
