using System.ComponentModel;

namespace Core.Enum
{
    public enum TransactionType
    {
        [Description("Paystack")]
        Paystack = 1,
        [Description("Transfer")]
        Transfer = 2,
    }
}
