using Rinha.Entities;

namespace Rinha.Data.Redis
{
    public interface IRedisService
    {

        Pessoa? GetData(Guid id);

        bool SetData(Pessoa pessoa);

        object RemoveData(Guid id);
    }
}