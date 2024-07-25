using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Core.Enum
{
    public enum ApplicationStatus
    {
        [Description("Accepted")]
        Accepted = 1,
        [Description("Rejected")]
        Rejected = 2,
        [Description("Pending")]
        Pending = 3,
        [Description("TakeInterview")]
        TakeInterview = 4,
    }
}
