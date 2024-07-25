using System.ComponentModel;

namespace Core.Enum
{
    public enum JobType
    {
      
        [Description("Full-time (On Premise)")]
        Full_Time_Premise = 1,
        [Description("Part-time (On Premise)")]
        Part_Time_Premise,
        [Description("Full-time (Work from Home)")]
        Full_Time_Home,
        [Description("Part-time (work from Home)")]
        Part_Time_Home,
    }
}
