using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Config
{
	public class GeneralConfiguration: IGeneralConfiguration
    {
        public string AdminEmail { get; set; }
        public int TimeDifference { get; set; }
        public string PayStakApiKey { get; set; }
		public string RedisConfiguration { get; set; }
		public string RedisInstanceName { get; set; }
		public string RedisPassword { get; set; }
		public bool RedisAbortOnConnectFail { get; set; }
		public bool RedisAllowAdmin { get; set; }
	}
}
