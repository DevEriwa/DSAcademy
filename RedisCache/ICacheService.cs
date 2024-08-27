using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisCache
{
	public interface ICacheService
	{
		bool Disabled { get; set; }
		T GetFromCacheAddIfNotFound<T>(string key, Func<T> missedCacheCall, TimeSpan? timeToLive);
		Task<T> GetFromCacheAddIfNotFoundAsync<T>(string key, Func<Task<T>> missedCacheCall, TimeSpan? timeToLive);
		void Set(string key, object val, TimeSpan? timeToLive);
		void Remove(string key);
		//void Clear();
		void SetAndOverrideFromCacheIfFound(string key, object val, TimeSpan? timeToLive);
	}
}
