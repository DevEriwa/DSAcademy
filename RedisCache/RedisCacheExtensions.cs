using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisCache
{
	public static class RedisCacheExtensions
	{
		public static T Get<T>(this IDatabase cache, string key)
		{
			return Deserialize<T>(cache.StringGet(key));
		}

		public async static Task<T> GetAsync<T>(this IDatabase cache, string key)
		{
			return Deserialize<T>(await cache.StringGetAsync(key));
		}

		public static void Set(this IDatabase cache, string key, object value, TimeSpan? timeToLive)
		{
			cache.StringSet(key, Serialize(value), timeToLive);
		}

		static string Serialize(object o)
		{
			if (o == null)
			{
				return null;
			}

			return JsonConvert.SerializeObject(o, new JsonSerializerSettings()
			{
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			});
		}

		static T Deserialize<T>(string o)
		{
			if (o == null)
			{
				return default(T);
			}

			return JsonConvert.DeserializeObject<T>(o);
		}
	}
}
