using Core.Db;
using Core.Enum;
using Core.Models;
using Core.ViewModels;
using Logic.IHelpers;
using Logic.PaymentGateways;
using Microsoft.EntityFrameworkCore;

namespace Logic.Services
{
    /// <summary>
    /// Resolves the correct payment gateway based on the school's CompanySetting.PaymentProvider.
    /// Also handles gateway settings persistence.
    /// </summary>
    public class PaymentGatewayFactory
    {
        private readonly AppDbContext _context;
        private readonly IHttpClientFactory _http;

        public PaymentGatewayFactory(AppDbContext context, IHttpClientFactory http)
        {
            _context = context;
            _http = http;
        }

        // ── Resolve gateway for a school ─────────────────────────────────────
        public IPaymentGatewayService GetGateway(CompanySetting settings) =>
            settings.PaymentProvider switch
            {
                PaymentProvider.Paystack    => new PaystackGatewayService(_http),
                PaymentProvider.Flutterwave => new FlutterwaveGatewayService(_http),
                PaymentProvider.Stripe      => new StripeGatewayService(_http),
                PaymentProvider.PayPal      => new PayPalGatewayService(_http),
                _                           => throw new InvalidOperationException("This school uses manual bank transfer — no gateway configured.")
            };

        // ── Load school settings ──────────────────────────────────────────────
        public CompanySetting? GetSettings(Guid companyId) =>
            _context.CompanySettings.FirstOrDefault(s => s.CompanyId == companyId);

        // ── Initialize a payment ──────────────────────────────────────────────
        public async Task<PaymentInitResponse> InitializeAsync(PaymentRequest request)
        {
            var settings = GetSettings(request.CompanyId);
            if (settings == null)
                return new PaymentInitResponse { Success = false, ErrorMessage = "School payment settings not configured." };

            if (settings.PaymentProvider == PaymentProvider.Manual)
                return new PaymentInitResponse { Success = false, ErrorMessage = "MANUAL", ProviderName = "Manual" };

            var gateway = GetGateway(settings);
            return await gateway.InitializePaymentAsync(request, settings);
        }

        // ── Verify a payment ─────────────────────────────────────────────────
        public async Task<bool> VerifyAsync(string reference, Guid companyId)
        {
            var settings = GetSettings(companyId);
            if (settings == null || settings.PaymentProvider == PaymentProvider.Manual)
                return false;

            var gateway = GetGateway(settings);
            return await gateway.VerifyPaymentAsync(reference, settings);
        }

        // ── Save gateway settings ─────────────────────────────────────────────
        public bool SaveGatewaySettings(PaymentGatewaySettingsViewModel model)
        {
            try
            {
                var settings = _context.CompanySettings
                    .FirstOrDefault(s => s.CompanyId == model.CompanyId);

                if (settings == null)
                {
                    settings = new CompanySetting { CompanyId = model.CompanyId };
                    _context.CompanySettings.Add(settings);
                }

                settings.PaymentProvider        = model.Provider;
                settings.IsTestMode             = model.IsTestMode;
                settings.PaystackPublicKey      = model.PaystackPublicKey;
                settings.PaystackSecretKey      = model.PaystackSecretKey;
                settings.FlutterwavePublicKey   = model.FlutterwavePublicKey;
                settings.FlutterwaveSecretKey   = model.FlutterwaveSecretKey;
                settings.FlutterwaveEncryptionKey = model.FlutterwaveEncryptionKey;
                settings.StripePublishableKey   = model.StripePublishableKey;
                settings.StripeSecretKey        = model.StripeSecretKey;
                settings.StripeWebhookSecret    = model.StripeWebhookSecret;
                settings.PayPalClientId         = model.PayPalClientId;
                settings.PayPalClientSecret     = model.PayPalClientSecret;

                _context.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        // ── Get current gateway settings ViewModel ─────────────────────────────
        public PaymentGatewaySettingsViewModel? GetGatewaySettingsViewModel(Guid companyId)
        {
            var s = GetSettings(companyId);
            if (s == null) return new PaymentGatewaySettingsViewModel { CompanyId = companyId };

            return new PaymentGatewaySettingsViewModel
            {
                CompanyId               = companyId,
                Provider                = s.PaymentProvider,
                IsTestMode              = s.IsTestMode,
                PaystackPublicKey       = s.PaystackPublicKey,
                PaystackSecretKey       = s.PaystackSecretKey,
                FlutterwavePublicKey    = s.FlutterwavePublicKey,
                FlutterwaveSecretKey    = s.FlutterwaveSecretKey,
                FlutterwaveEncryptionKey = s.FlutterwaveEncryptionKey,
                StripePublishableKey    = s.StripePublishableKey,
                StripeSecretKey         = s.StripeSecretKey,
                StripeWebhookSecret     = s.StripeWebhookSecret,
                PayPalClientId          = s.PayPalClientId,
                PayPalClientSecret      = s.PayPalClientSecret
            };
        }
    }
}
