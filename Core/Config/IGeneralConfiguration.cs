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
    }
}
