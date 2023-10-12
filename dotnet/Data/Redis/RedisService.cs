
using Rinha.Entities;
using StackExchange.Redis;
using System.Text.Json;

namespace Rinha.Data.Redis
{
    public class RedisService : IRedisService
    {
        private readonly IDatabase _db;
        public RedisService(IDatabase cache)
        {
            _db = cache;
        }
        public Pessoa? _GetData(Guid id)
        {
            var key = id.ToString();

            var value = _db.StringGet(key);

            if (!string.IsNullOrEmpty(value))
            {
                return JsonSerializer.Deserialize<Pessoa>(value);
            };
            return null;
        }
        public bool _SetData(Pessoa pessoa)
        {
            var isSet = _db.StringSet(pessoa.Id.ToString(), JsonSerializer.Serialize(pessoa));
            return isSet;
        }
        public object RemoveData(Guid id)
        {
            var key = id.ToString();
            bool _isKeyExist = _db.KeyExists(key);
            if (_isKeyExist == true)
            {
                return _db.KeyDelete(key);
            }
            return false;
        }

        public string? GetData(string key)
        {
            var value = _db.StringGet(key);
            return value;
        }

        public bool SetData(string key, string value)
        {
            return _db.StringSet(key, value);
        }
    }
}