using Core.Config;
using Core.Db;
using Core.Enum;
using Core.Models;
using Core.ViewModels;
using Logic.IHelpers;
using Logic.SmtpMailServices;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace Logic.Helpers
{
    public class PaystackHelper : IPaystackHelper
    {
        private readonly IUserHelper _userHelper;
        private readonly IEmailService _emailService;
        private readonly AppDbContext _context;
        protected RestRequest request;
        public static string RestUrl = "https://api.paystack.co/";
        private RestClient client;
        static string ApiEndPoint = "";
        private readonly IGeneralConfiguration _generalConfiguration;

		public PaystackHelper(
			IUserHelper userHelper,
			IEmailService emailService,
			AppDbContext context,
			IGeneralConfiguration generalConfiguration)
		{
			_userHelper = userHelper;
			_emailService = emailService;
			_context = context;
			_generalConfiguration = generalConfiguration;
		}


		public async Task<PaystackRepsonse> GeneratePaymentParameters(TrainingCourse payData, ApplicationUserViewModel user)
        {
            try
            {
                var totalAmount = 0.0;
                var paystackResponse = MakeOrderPayment(payData, user,totalAmount);
                if (paystackResponse != null)
                {
                    var userPayment = _context.Payments.Where(p => p.InvoiceNumber == paystackResponse.data.reference).FirstOrDefault();
                    Paystack paystack = new Paystack()
                    {
                        PaymentsHistory = userPayment,
                        authorization_url = paystackResponse.data.authorization_url,
                        access_code = paystackResponse.data.access_code,
                        PaymentHistoryId = userPayment.Id,
                        amount = Convert.ToDecimal(payData.Amount),
                        transaction_date = DateTime.Now,
                        reference = paystackResponse.data.reference,
                    };
                    _context.Paystacks.Add(paystack);
                    await _context.SaveChangesAsync();
                    return paystackResponse;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                var toEmail = _generalConfiguration.AdminEmail;
                var subject = "Generate Payment Parameters Method Exception on SmartEnter";
                _emailService.SendEmail(toEmail, subject, ex.Message);
                throw;
            }
        }
        public PaystackRepsonse MakeOrderPayments(TrainingCourse payData, ApplicationUserViewModel user,double amount)
        {
            try
            {
                var amounts = payData.Amount;
                var referenceId = Generate().ToString();
                var createHistory = new Payments()
                {
                    UserId = user.Id,
                    CourseId = payData.Id,
                    Source = TransactionType.Paystack,
                    ProveOfPayment = "N/A",
                    Status = PaymentStatus.Pending,
                    DateCreated = DateTime.Now,
                    InvoiceNumber = referenceId,
                };
                _context.Payments.Add(createHistory);
                _context.SaveChanges();

                var payStack = "sk_test_8fdf5e980f4738f061f7d70ec8156ff79344d146";

				PaystackRepsonse paystackRepsonse = null;
                long milliseconds = DateTime.Now.Ticks;
                string testid = milliseconds.ToString();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ApiEndPoint = "/transaction/initialize";
                request = new RestRequest(ApiEndPoint, Method.Post);
                request.AddHeader("accept", "application/json");
                request.AddHeader("Authorization", "Bearer " + payStack);
                request.AddParameter("reference", referenceId);
                double total = (double)amounts;
                request.AddParameter("amount", total * 100);

                List<CustomeField> myCustomfields = new List<CustomeField>();
                CustomeField nameCustomeField = new CustomeField()
                {
                    display_name = "Email",
                    variable_name = "Email",
                    value = user.Email,
                };
                myCustomfields.Add(nameCustomeField);
                Dictionary<string, List<CustomeField>> metadata = new Dictionary<string, List<CustomeField>>();
                metadata.Add("custom_fields", myCustomfields);
                var serializedMetadata = JsonConvert.SerializeObject(metadata);
                request.AddParameter("metadata", serializedMetadata);
                request.AddParameter("email", user.Email);
                var serializedRequest = JsonConvert.SerializeObject(request, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                var result = client.ExecuteAsync(request).Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    paystackRepsonse = JsonConvert.DeserializeObject<PaystackRepsonse>(result.Content);
                }
                return paystackRepsonse;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
		public PaystackRepsonse MakeOrderPayment(TrainingCourse payData, ApplicationUserViewModel user, double amount)
		{
			try
			{
				// Validate input parameters
				if (payData == null || user == null)
				{
					throw new ArgumentNullException("Payment data or user information is missing.");
				}

				var amounts = payData.Amount;
				var referenceId = Generate().ToString();

				// Create payment history
				var createHistory = new Payments()
				{
					UserId = user.Id,
					CourseId = payData.Id,
					Source = TransactionType.Paystack,
					ProveOfPayment = "N/A",
					Status = PaymentStatus.Pending,
					DateCreated = DateTime.Now,
					InvoiceNumber = referenceId,
				};

				_context.Payments.Add(createHistory);
				_context.SaveChanges();

				// Paystack API secret key
				var payStackSecret = "sk_test_8fdf5e980f4738f061f7d70ec8156ff79344d146";

				// Initialize Paystack response
				PaystackRepsonse paystackRepsonse = null;

				// Configure the RestClient and RestRequest for Paystack API
				var client = new RestClient("https://api.paystack.co");
				var request = new RestRequest("/transaction/initialize", Method.Post);
				request.AddHeader("accept", "application/json");
				request.AddHeader("Authorization", "Bearer " + payStackSecret);

				// Add parameters to the request
				request.AddParameter("reference", referenceId);
				double total = (double)amounts;
				request.AddParameter("amount", total * 100);
				request.AddParameter("email", user.Email);

				// Prepare metadata
				var metadata = new
				{
					custom_fields = new[]
					{
				new { display_name = "Email", variable_name = "Email", value = user.Email }
			}
				};
				request.AddParameter("metadata", JsonConvert.SerializeObject(metadata));

				// Execute the request asynchronously and handle the response
				var result = client.ExecuteAsync(request).Result;

				// Check for a successful response
				if (result.StatusCode == HttpStatusCode.OK)
				{
					paystackRepsonse = JsonConvert.DeserializeObject<PaystackRepsonse>(result.Content);
				}
				else
				{
					Console.WriteLine($"Paystack API call failed with status code {result.StatusCode}: {result.Content}");
				}

				return paystackRepsonse;
			}
			catch (Exception ex)
			{
				// Log the exception details for debugging
				Console.WriteLine($"Exception occurred in MakeOrderPayment: {ex.Message}");
				throw;
			}
		}

		public static int Generate()
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            return rand.Next(100000000, 999999999);
        }

        public async Task<PaystackRepsonse> VerifyPayment(Paystack payment)
        {
            PaystackRepsonse paystackRepsonse = null;
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ApiEndPoint = "/transaction/verify/" + payment.reference;
                request = new RestRequest(ApiEndPoint, Method.Get);
                request.AddHeader("accept", "application/json");
                request.AddHeader("Authorization", "Bearer " + _generalConfiguration.PayStakApiKey);
                var result = client.ExecuteAsync(request).Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    paystackRepsonse = JsonConvert.DeserializeObject<PaystackRepsonse>(result.Content);
                    var payStack = UpdateResponse(paystackRepsonse);

                }
                return paystackRepsonse;
            }
            catch (Exception ex)
            {
                var toEmail = _generalConfiguration.AdminEmail;
                var subject = "Paystack Main Payment Verification Exception on SmartEnter";
                _emailService.SendEmail(toEmail, subject, ex.Message);
                throw;
            }
        }
        public Paystack UpdateResponse(PaystackRepsonse PaystackRepsonse)
        {
            try
            {
                Paystack paystackEntity = _context.Paystacks.Where(p => p.reference == PaystackRepsonse.data.reference)
                                            ?.Include(s => s.PaymentsHistory)?.OrderBy(s => s.amount)?.FirstOrDefault();
                if (paystackEntity != null)
                {
                    paystackEntity.bank = PaystackRepsonse.data.authorization.bank;
                    paystackEntity.brand = PaystackRepsonse.data.authorization.brand;
                    paystackEntity.card_type = PaystackRepsonse.data.authorization.card_type;
                    paystackEntity.channel = PaystackRepsonse.data.channel;
                    paystackEntity.country_code = PaystackRepsonse.data.authorization.country_code;
                    paystackEntity.currency = PaystackRepsonse.data.currency;
                    paystackEntity.domain = PaystackRepsonse.data.domain;
                    paystackEntity.exp_month = PaystackRepsonse.data.authorization.exp_month;
                    paystackEntity.exp_year = PaystackRepsonse.data.authorization.exp_year;
                    paystackEntity.fees = PaystackRepsonse.data.fees.ToString();
                    paystackEntity.gateway_response = PaystackRepsonse.data.gateway_response;
                    paystackEntity.ip_address = PaystackRepsonse.data.ip_address;
                    paystackEntity.last4 = PaystackRepsonse.data.authorization.last4;
                    paystackEntity.message = PaystackRepsonse.message;
                    paystackEntity.reference = PaystackRepsonse.data.reference;
                    paystackEntity.reusable = PaystackRepsonse.data.authorization.reusable;
                    paystackEntity.signature = PaystackRepsonse.data.authorization.signature;
                    paystackEntity.status = PaystackRepsonse.data.status;
                    paystackEntity.transaction_date = PaystackRepsonse.data.transaction_date;

                    _context.Update(paystackEntity);
                    _context.SaveChanges();

                    var payment = _context.Payments.Where(x => x.InvoiceNumber == PaystackRepsonse.data.reference).FirstOrDefault();
                    if (payment != null)
                    {
                        payment.Status = PaymentStatus.Approved;
                        _context.Payments.Update(payment);
                        _context.SaveChanges();
                    }
                }
                return paystackEntity;
            }
            catch (Exception ex)
            {
                var toEmail = _generalConfiguration.AdminEmail;
                var subject = "Paystack Updating Response Exception on SmartEnter";
                _emailService.SendEmail(toEmail, subject, ex.Message);
                throw;
            }
        }
    }
}
