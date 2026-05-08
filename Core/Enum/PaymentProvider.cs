namespace Core.Enum
{
    public enum PaymentProvider
    {
        Manual = 0,      // Bank transfer / proof of payment (existing flow)
        Paystack = 1,
        Flutterwave = 2,
        Stripe = 3,
        PayPal = 4
    }
}
