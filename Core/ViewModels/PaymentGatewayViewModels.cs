using Core.Enum;

namespace Core.ViewModels
{
    /// <summary>School Admin gateway settings form.</summary>
    public class PaymentGatewaySettingsViewModel
    {
        public Guid CompanyId { get; set; }
        public PaymentProvider Provider { get; set; } = PaymentProvider.Manual;
        public bool IsTestMode { get; set; } = true;

        // Paystack
        public string? PaystackPublicKey { get; set; }
        public string? PaystackSecretKey { get; set; }

        // Flutterwave
        public string? FlutterwavePublicKey { get; set; }
        public string? FlutterwaveSecretKey { get; set; }
        public string? FlutterwaveEncryptionKey { get; set; }

        // Stripe
        public string? StripePublishableKey { get; set; }
        public string? StripeSecretKey { get; set; }
        public string? StripeWebhookSecret { get; set; }

        // PayPal
        public string? PayPalClientId { get; set; }
        public string? PayPalClientSecret { get; set; }
    }

    /// <summary>Returned by gateway when a payment is initialized.</summary>
    public class PaymentInitResponse
    {
        public bool Success { get; set; }
        public string? PaymentUrl { get; set; }   // Redirect URL for hosted checkout
        public string? Reference { get; set; }    // Transaction reference to verify later
        public string? ErrorMessage { get; set; }
        public string? ProviderName { get; set; }
        public string? PublicKey { get; set; }    // For client-side SDKs (Stripe, Paystack inline)
        public decimal Amount { get; set; }
        public string? Email { get; set; }
        public string? Currency { get; set; } = "NGN";
    }

    /// <summary>Request sent to the gateway service.</summary>
    public class PaymentRequest
    {
        public Guid CompanyId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "NGN";
        public string Reference { get; set; } = Guid.NewGuid().ToString("N");
        public string CallbackUrl { get; set; } = string.Empty;
        public int CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty;
    }
}
