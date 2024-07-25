using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Core.Enum
{
	public enum ProjectStatus
	{
        [Description("New")]
        New = 1,
        [Description("Started")]
        Started = 2,
        [Description("Accepted")]
        Accepted = 3,
    }
}
