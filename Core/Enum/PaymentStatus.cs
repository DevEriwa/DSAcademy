using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Core.Enum
{
    public enum PaymentStatus
    {
        [Description("Approved")]
        Approved = 1,
        [Description("Declined")]
        Declined = 2,
        [Description("Pending")]
        Pending = 3,
    }
}
