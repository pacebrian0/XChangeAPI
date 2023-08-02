namespace XChangeAPI.Services.Interfaces
{
    public interface ICacheService
    {
        Task<T> GetData<T>(string key);
        Task RemoveData(string key);
        Task SetData<T>(string key, T value, DateTimeOffset expiration);
    }
}