using System.ComponentModel;

namespace Core.Enum
{
	public enum ProgramEnum
	{
		[Description("Frontend Developer")]
		Frontend = 1,
		[Description("Backend Developer")]
		Backend = 2,
		[Description("Full-Stack Developer")]
		FullStack = 3,
	}
}
