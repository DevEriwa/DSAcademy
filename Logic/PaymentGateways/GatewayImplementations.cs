using Core.Models;
using Core.ViewModels;
using Logic.IHelpers;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Logic.PaymentGateways
{
    // ────────────────────────────────────────────────────────────────────────────
    // PAYSTACK
    // ────────────────────────────────────────────────────────────────────────────
    public class PaystackGatewayService : IPaymentGatewayService
    {
        public string ProviderName => "Paystack";
        private readonly IHttpClientFactory _http;
        public PaystackGatewayService(IHttpClientFactory http) => _http = http;

        public async Task<PaymentInitResponse> InitializePaymentAsync(PaymentRequest req, CompanySetting s)
        {
            try
            {
                var client = _http.CreateClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", s.PaystackSecretKey);

                var body = new
                {
                    email = req.Email,
                    amount = (int)(req.Amount * 100), // kobo
                    reference = req.Reference,
                    callback_url = req.CallbackUrl,
                    metadata = new { course_id = req.CourseId, full_name = req.FullName }
                };

                var res = await client.PostAsync(
                    "https://api.paystack.co/transaction/initialize",
                    new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json"));

                var json = JsonDocument.Parse(await res.Content.ReadAsStringAsync());
                var data = json.RootElement.GetProperty("data");

                return new PaymentInitResponse
                {
                    Success = true,
                    PaymentUrl = data.GetProperty("authorization_url").GetString(),
                    Reference = req.Reference,
                    ProviderName = ProviderName,
                    PublicKey = s.PaystackPublicKey,
                    Amount = req.Amount,
                    Email = req.Email,
                    Currency = "NGN"
                };
            }
            catch (Exception ex)
            {
                return new PaymentInitResponse { Success = false, ErrorMessage = ex.Message };
            }
        }

        public async Task<bool> VerifyPaymentAsync(string reference, CompanySetting s)
        {
            try
            {
                var client = _http.CreateClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", s.PaystackSecretKey);

                var res = await client.GetAsync($"https://api.paystack.co/transaction/verify/{reference}");
                var json = JsonDocument.Parse(await res.Content.ReadAsStringAsync());
                var status = json.RootElement.GetProperty("data").GetProperty("status").GetString();
                return status == "success";
            }
            catch { return false; }
        }
    }

    // ────────────────────────────────────────────────────────────────────────────
    // FLUTTERWAVE
    // ────────────────────────────────────────────────────────────────────────────
    public class FlutterwaveGatewayService : IPaymentGatewayService
    {
        public string ProviderName => "Flutterwave";
        private readonly IHttpClientFactory _http;
        public FlutterwaveGatewayService(IHttpClientFactory http) => _http = http;

        public async Task<PaymentInitResponse> InitializePaymentAsync(PaymentRequest req, CompanySetting s)
        {
            try
            {
                var client = _http.CreateClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", s.FlutterwaveSecretKey);

                var body = new
                {
                    tx_ref = req.Reference,
                    amount = req.Amount,
                    currency = req.Currency,
                    redirect_url = req.CallbackUrl,
                    customer = new { email = req.Email, name = req.FullName },
                    customizations = new { title = req.CourseName }
                };

                var res = await client.PostAsync(
                    "https://api.flutterwave.com/v3/payments",
                    new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json"));

                var json = JsonDocument.Parse(await res.Content.ReadAsStringAsync());
                var link = json.RootElement.GetProperty("data").GetProperty("link").GetString();

                return new PaymentInitResponse
                {
                    Success = true,
                    PaymentUrl = link,
                    Reference = req.Reference,
                    ProviderName = ProviderName,
                    Amount = req.Amount,
                    Email = req.Email
                };
            }
            catch (Exception ex)
            {
                return new PaymentInitResponse { Success = false, ErrorMessage = ex.Message };
            }
        }

        public async Task<bool> VerifyPaymentAsync(string reference, CompanySetting s)
        {
            try
            {
                var client = _http.CreateClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", s.FlutterwaveSecretKey);

                var res = await client.GetAsync(
                    $"https://api.flutterwave.com/v3/transactions/{reference}/verify");
                var json = JsonDocument.Parse(await res.Content.ReadAsStringAsync());
                var status = json.RootElement.GetProperty("data").GetProperty("status").GetString();
                return status?.ToLower() == "successful";
            }
            catch { return false; }
        }
    }

    // ────────────────────────────────────────────────────────────────────────────
    // STRIPE
    // ────────────────────────────────────────────────────────────────────────────
    public class StripeGatewayService : IPaymentGatewayService
    {
        public string ProviderName => "Stripe";
        private readonly IHttpClientFactory _http;
        public StripeGatewayService(IHttpClientFactory http) => _http = http;

        public async Task<PaymentInitResponse> InitializePaymentAsync(PaymentRequest req, CompanySetting s)
        {
            try
            {
                var client = _http.CreateClient();
                // Stripe uses Basic auth with secret key as username
                var encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{s.StripeSecretKey}:"));
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Basic", encoded);

                // Create a Stripe Checkout Session
                var form = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    ["payment_method_types[]"] = "card",
                    ["mode"] = "payment",
                    ["line_items[0][price_data][currency]"] = req.Currency.ToLower(),
                    ["line_items[0][price_data][unit_amount]"] = ((int)(req.Amount * 100)).ToString(),
                    ["line_items[0][price_data][product_data][name]"] = req.CourseName,
                    ["line_items[0][quantity]"] = "1",
                    ["success_url"] = req.CallbackUrl + "?reference=" + req.Reference + "&status=success",
                    ["cancel_url"] = req.CallbackUrl + "?reference=" + req.Reference + "&status=cancelled",
                    ["client_reference_id"] = req.Reference,
                    ["customer_email"] = req.Email
                });

                var res = await client.PostAsync("https://api.stripe.com/v1/checkout/sessions", form);
                var json = JsonDocument.Parse(await res.Content.ReadAsStringAsync());
                var url = json.RootElement.GetProperty("url").GetString();
                var sessionId = json.RootElement.GetProperty("id").GetString();

                return new PaymentInitResponse
                {
                    Success = true,
                    PaymentUrl = url,
                    Reference = sessionId ?? req.Reference,
                    ProviderName = ProviderName,
                    PublicKey = s.StripePublishableKey,
                    Amount = req.Amount,
                    Email = req.Email,
                    Currency = req.Currency
                };
            }
            catch (Exception ex)
            {
                return new PaymentInitResponse { Success = false, ErrorMessage = ex.Message };
            }
        }

        public async Task<bool> VerifyPaymentAsync(string sessionId, CompanySetting s)
        {
            try
            {
                var client = _http.CreateClient();
                var encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{s.StripeSecretKey}:"));
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Basic", encoded);

                var res = await client.GetAsync($"https://api.stripe.com/v1/checkout/sessions/{sessionId}");
                var json = JsonDocument.Parse(await res.Content.ReadAsStringAsync());
                var status = json.RootElement.GetProperty("payment_status").GetString();
                return status == "paid";
            }
            catch { return false; }
        }
    }

    // ────────────────────────────────────────────────────────────────────────────
    // PAYPAL
    // ────────────────────────────────────────────────────────────────────────────
    public class PayPalGatewayService : IPaymentGatewayService
    {
        public string ProviderName => "PayPal";
        private readonly IHttpClientFactory _http;
        public PayPalGatewayService(IHttpClientFactory http) => _http = http;

        private string BaseUrl(bool isTest) =>
            isTest ? "https://api-m.sandbox.paypal.com" : "https://api-m.paypal.com";

        private async Task<string> GetAccessToken(CompanySetting s)
        {
            var client = _http.CreateClient();
            var encoded = Convert.ToBase64String(
                Encoding.UTF8.GetBytes($"{s.PayPalClientId}:{s.PayPalClientSecret}"));
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", encoded);

            var res = await client.PostAsync(
                $"{BaseUrl(s.IsTestMode)}/v1/oauth2/token",
                new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("grant_type", "client_credentials") }));

            var json = JsonDocument.Parse(await res.Content.ReadAsStringAsync());
            return json.RootElement.GetProperty("access_token").GetString() ?? "";
        }

        public async Task<PaymentInitResponse> InitializePaymentAsync(PaymentRequest req, CompanySetting s)
        {
            try
            {
                var token = await GetAccessToken(s);
                var client = _http.CreateClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var body = new
                {
                    intent = "CAPTURE",
                    purchase_units = new[] { new { amount = new { currency_code = req.Currency, value = req.Amount.ToString("F2") }, description = req.CourseName } },
                    application_context = new { return_url = req.CallbackUrl + "?reference=" + req.Reference, cancel_url = req.CallbackUrl + "?cancelled=true" }
                };

                var res = await client.PostAsync(
                    $"{BaseUrl(s.IsTestMode)}/v2/checkout/orders",
                    new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json"));

                var json = JsonDocument.Parse(await res.Content.ReadAsStringAsync());
                var orderId = json.RootElement.GetProperty("id").GetString();
                var approveLink = json.RootElement.GetProperty("links").EnumerateArray()
                    .FirstOrDefault(l => l.GetProperty("rel").GetString() == "approve")
                    .GetProperty("href").GetString();

                return new PaymentInitResponse
                {
                    Success = true,
                    PaymentUrl = approveLink,
                    Reference = orderId ?? req.Reference,
                    ProviderName = ProviderName,
                    Amount = req.Amount,
                    Email = req.Email,
                    Currency = req.Currency
                };
            }
            catch (Exception ex)
            {
                return new PaymentInitResponse { Success = false, ErrorMessage = ex.Message };
            }
        }

        public async Task<bool> VerifyPaymentAsync(string orderId, CompanySetting s)
        {
            try
            {
                var token = await GetAccessToken(s);
                var client = _http.CreateClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                // Capture the order
                var res = await client.PostAsync(
                    $"{BaseUrl(s.IsTestMode)}/v2/checkout/orders/{orderId}/capture",
                    new StringContent("{}", Encoding.UTF8, "application/json"));

                var json = JsonDocument.Parse(await res.Content.ReadAsStringAsync());
                var status = json.RootElement.GetProperty("status").GetString();
                return status == "COMPLETED";
            }
            catch { return false; }
        }
    }
}
