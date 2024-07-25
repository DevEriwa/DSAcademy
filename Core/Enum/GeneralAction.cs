using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Core.Enum
{
    public enum GeneralAction
    {
        [Description("CREATE")]
        CREATE = 1,
        [Description("EDIT")]
        EDIT,
        [Description("DELETE")]
        DELETE,
        [Description("ACTIVATE")]
        ACTIVATE,
        [Description("DEACTIVATE")]
        DEACTIVATE,
        [Description("UPDATE")]
        UPDATE,
    }
}
