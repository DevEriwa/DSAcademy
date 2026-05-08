using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
	public class CompanySetting
	{
		[Key]
		public Guid? CompanyId { get; set; }
		[Display(Name = "Company")]
		[ForeignKey("CompanyId")]
		public virtual Company? Company { get; set; }
		[Display(Name = "Enable Custom Invoice")]
		public bool EnableCustomInvoice { get; set; }
		[Display(Name = "Enable Web Module")]
		public double CharLengthPerPage { get; set; }
		[Display(Name = "Dashboard Url")]
		public string? DashboardUrl { get; set; }
		[Display(Name = "Enable Base Package")]
		public bool EnableBasePackage { get; set; }
		[Display(Name = "Enable SMS")]
		public bool EnableSMS { get; set; }

		// ── Theme / Branding Customisation ──────────────────────────────────
		/// <summary>Primary brand colour (e.g. navbar, buttons). Hex format: #0A192F</summary>
		[Display(Name = "Primary Color")]
		[MaxLength(10)]
		public string? PrimaryColor { get; set; } = "#0A192F";

		/// <summary>Secondary / accent colour (e.g. highlights, badges). Hex format: #FFB300</summary>
		[Display(Name = "Secondary Color")]
		[MaxLength(10)]
		public string? SecondaryColor { get; set; } = "#FFB300";

		/// <summary>Sidebar / panel background colour override.</summary>
		[Display(Name = "Sidebar Color")]
		[MaxLength(10)]
		public string? SidebarColor { get; set; } = "#112B50";

		/// <summary>School-specific font preference (e.g. 'Outfit', 'Roboto').</summary>
		[Display(Name = "Font Family")]
		[MaxLength(60)]
		public string? FontFamily { get; set; } = "Outfit";

		/// <summary>Dark mode toggle for the school dashboard.</summary>
		[Display(Name = "Dark Mode")]
		public bool DarkMode { get; set; } = false;

		// ── Payment Gateway Configuration ─────────────────────────────────────
		/// <summary>Which payment gateway this school uses.</summary>
		[Display(Name = "Payment Provider")]
		public Core.Enum.PaymentProvider PaymentProvider { get; set; } = Core.Enum.PaymentProvider.Manual;

		// Paystack
		[Display(Name = "Paystack Public Key")]
		[MaxLength(120)]
		public string? PaystackPublicKey { get; set; }

		[Display(Name = "Paystack Secret Key")]
		[MaxLength(120)]
		public string? PaystackSecretKey { get; set; }

		// Flutterwave
		[Display(Name = "Flutterwave Public Key")]
		[MaxLength(120)]
		public string? FlutterwavePublicKey { get; set; }

		[Display(Name = "Flutterwave Secret Key")]
		[MaxLength(120)]
		public string? FlutterwaveSecretKey { get; set; }

		[Display(Name = "Flutterwave Encryption Key")]
		[MaxLength(120)]
		public string? FlutterwaveEncryptionKey { get; set; }

		// Stripe
		[Display(Name = "Stripe Publishable Key")]
		[MaxLength(120)]
		public string? StripePublishableKey { get; set; }

		[Display(Name = "Stripe Secret Key")]
		[MaxLength(120)]
		public string? StripeSecretKey { get; set; }

		[Display(Name = "Stripe Webhook Secret")]
		[MaxLength(120)]
		public string? StripeWebhookSecret { get; set; }

		// PayPal
		[Display(Name = "PayPal Client ID")]
		[MaxLength(200)]
		public string? PayPalClientId { get; set; }

		[Display(Name = "PayPal Client Secret")]
		[MaxLength(200)]
		public string? PayPalClientSecret { get; set; }

		/// <summary>Use sandbox/test mode for the configured gateway.</summary>
		[Display(Name = "Test Mode")]
		public bool IsTestMode { get; set; } = true;
	}
}
