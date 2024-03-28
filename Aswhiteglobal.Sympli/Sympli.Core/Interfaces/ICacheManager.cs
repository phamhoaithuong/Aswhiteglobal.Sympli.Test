
namespace Sympli.Core.Interfaces
{
    public interface ICacheManager
    {
        Task<T?> GetDataAsync<T>(string key, CancellationToken token = default);
        Task SetDataAsync<T>(string key, T value, int minutes, CancellationToken token = default);
    }
}
