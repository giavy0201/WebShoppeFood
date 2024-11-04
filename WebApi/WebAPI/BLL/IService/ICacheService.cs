namespace BLL.IService
{
    public interface ICacheService
    {
        Task<T> GetCachedData<T>(string key);
        Task SetCachedData<T>(string key, T data, int timeMinutes);
        Task SetCachedData<T>(string key, T data);
        Task RemoveCache(string key);
    }
}
