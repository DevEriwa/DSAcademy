using Core.ViewModels;

namespace Logic.IHelpers
{
    /// <summary>
    /// Each payment gateway implements this contract.
    /// The factory resolves the correct implementation at runtime based on the school's settings.
    /// </summary>
    public interface IPaymentGatewayService
    {
        string ProviderName { get; }

        /// <summary>Initialize a payment session and return a redirect URL or inline config.</summary>
        Task<PaymentInitResponse> InitializePaymentAsync(PaymentRequest request, Core.Models.CompanySetting settings);

        /// <summary>Verify a payment after callback/redirect.</summary>
        Task<bool> VerifyPaymentAsync(string reference, Core.Models.CompanySetting settings);
    }
}
