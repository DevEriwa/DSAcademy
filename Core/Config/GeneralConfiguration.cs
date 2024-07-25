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
    }
}
