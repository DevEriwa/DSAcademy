using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Config
{
	public interface IGeneralConfiguration
	{
        string AdminEmail { get; set; }
        int TimeDifference { get; set; }
        string PayStakApiKey { get; set; }
		bool RedisAbortOnConnectFail { get; set; }
		bool RedisAllowAdmin { get; set; }
		string RedisConfiguration { get; set; }
		string RedisInstanceName { get; set; }
		string RedisPassword { get; set; }
	}
}
