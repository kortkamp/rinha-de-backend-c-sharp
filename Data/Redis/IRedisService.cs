using Rinha.Entities;

namespace Rinha.Data.Redis
{
    public interface IRedisService
    {

        string? GetData(string id);

        bool SetData(string key, string value);

    }
}