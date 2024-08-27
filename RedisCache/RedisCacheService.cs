using Core.Config;
using StackExchange.Redis;
using System.Configuration;
namespace RedisCache
{
	public class RedisCacheService : ICacheService
	{
		private static IGeneralConfiguration _generalConfiguration;
		public RedisCacheService(IGeneralConfiguration generalConfiguration)
		{
			_generalConfiguration = generalConfiguration;
		}

		public string CustomizeRedisKey(string key)
		{
			var connection = _generalConfiguration.RedisInstanceName;
			var customizeRedisKey = $"{connection}_{key}";
			return customizeRedisKey;
		}
		public T Get<T>(string key)
		{
			try
			{
				IDatabase cache = Connection.GetDatabase();
				var customizeRedisKey = CustomizeRedisKey(key);
				var obj = cache.Get<T>(customizeRedisKey);
				return obj;
			}
			catch (Exception ex)
			{
				return default;
			}
		}

		public T GetFromCacheAddIfNotFound<T>(string key, Func<T> missedCacheCall, TimeSpan? timeToLive)
		{
			try
			{
				IDatabase cache = Connection.GetDatabase();
				var customizeRedisKey = CustomizeRedisKey(key);
				var obj = cache.Get<T>(customizeRedisKey);
				var defaultValue = default(T);
				var check = EqualityComparer<T>.Default.Equals(obj, defaultValue);
				if (check)
				{
					obj = missedCacheCall();
					if (obj != null)
					{
						cache.Set(customizeRedisKey, obj, timeToLive);
					}
					return obj;

				}
				return obj;

			}
			catch (Exception ex)
			{
				return missedCacheCall();
			}

		}
		public void SetAndOverrideFromCacheIfFound(string key, object val, TimeSpan? timeToLive)
		{
			try
			{
				IDatabase cache = Connection.GetDatabase();
				var customizeRedisKey = CustomizeRedisKey(key);
				Remove(customizeRedisKey);
				cache.Set(customizeRedisKey, val, timeToLive);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public async Task<T> GetFromCacheAddIfNotFoundAsync<T>(string key, Func<Task<T>> missedCacheCall, TimeSpan? timeToLive)
		{
			try
			{
				IDatabase cache = Connection.GetDatabase();
				var customizeRedisKey = CustomizeRedisKey(key);
				var obj = await cache.GetAsync<T>(customizeRedisKey);
				if (obj == null)
				{
					obj = await missedCacheCall();
					if (obj != null)
					{
						cache.Set(customizeRedisKey, obj, timeToLive);
					}
				}
				return obj;
			}
			catch (Exception ex)
			{
				return await missedCacheCall();
			}
		}

		public void Set(string key, object val, TimeSpan? timeToLive)
		{
			try
			{
				var customizeRedisKey = CustomizeRedisKey(key);
				IDatabase cache = Connection.GetDatabase();
				cache.Set(customizeRedisKey, val, timeToLive);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public void Remove(string key)
		{
			try
			{
				var customizeRedisKey = CustomizeRedisKey(key);
				IDatabase cache = Connection.GetDatabase();
				cache.KeyDelete(customizeRedisKey);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static Lazy<ConnectionMultiplexer> LazyConnection = new Lazy<ConnectionMultiplexer>(() =>
		{
			var connection = "127.0.0.1:6379";
			if (string.IsNullOrEmpty(connection))
			{
				throw new Exception("Please provide a Redis connection string in the RedisConnection AppSettings");
			}
			var options = ConfigurationOptions.Parse(connection);
			options.Password = _generalConfiguration.RedisPassword;
			options.AbortOnConnectFail = _generalConfiguration.RedisAbortOnConnectFail;
			options.AllowAdmin = _generalConfiguration.RedisAllowAdmin;
			return ConnectionMultiplexer.Connect(connection);
		});

		public static ConnectionMultiplexer Connection
		{
			get
			{
				return LazyConnection.Value;
			}
		}

		private bool? _disabled = null;


		public bool Disabled
		{
			get
			{
				if (_disabled == null)
				{
					_disabled = ConfigurationManager.AppSettings["BypassCache"] == "true";
				}

				return _disabled.Value;
			}
			set
			{
				_disabled = value;
			}
		}
	}
}
