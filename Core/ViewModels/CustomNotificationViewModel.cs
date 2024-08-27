using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
	public class CustomNotificationViewModel
	{
		public Guid CompanyId { get; set; }
		public string? CompanyName { get; set; }
		[Display(Name = "Page Size")]
		public int? SetPageSize { get; set; }
		[Display(Name = "Company Work Hour Per Day")]
		public int? CompanyWorkHourPerDay { get; set; }
		[Display(Name = "Company Invoice Terms")]
		public string? CompanyInvoiceTerms { get; set; }
		[Display(Name = "MOT Reminder Days")]
		public int? MOtReminderDays { get; set; }
		[Display(Name = "Enable MOT Reminder")]
		public bool IsMOTReminderEnabled { get; set; }
		[Display(Name = "Enable Reminder")]
		public bool IsReminderEnabled { get; set; }
		[Display(Name = "Enable Receipt Water Mark")]
		public bool IsReceiptWaterMark { get; set; }
		[Display(Name = "Mark-Up Percentage")]
		public long? PriceMarkUp { get; set; }
		[Display(Name = "Enable Price Mark-Up")]
		public bool IsPriceMarkUp { get; set; }
		[Display(Name = "QR Link")]
		public string? QRLink { get; set; }
		[Display(Name = "QR Description")]
		public string? QRDescription { get; set; }
		public string? FacebookAccessToken { get; set; }
		[Display(Name = "App Id")]
		public string? FacebookAppId { get; set; }
		[Display(Name = "App Secret Key")]
		public string? FacebookSecretKey { get; set; }
		public string? GoogleMapId { get; set; }
		[Display(Name = "Take Quick VAT From Cost")]
		public bool DisableVATForQuickVisit { get; set; }
		[Display(Name = "Hourly Rate")]
		public decimal? HourlyRate { get; set; }
		[Display(Name = "Receipt Colour")]
		public string? ReceiptColour { get; set; }
		[Display(Name = "Receipt Sub Color")]
		public string? ReceiptSubColour { get; set; }
		[Display(Name = "Enable Mark VHC Parts Green")]
		public bool MarkVHCPartsGreen { get; set; }
		[Display(Name = "Don't Pull Customer Detail When Fetching Car")]
		public bool DontPullCustomerDetailWhenFetchingCar { get; set; }
		public TimeSpan LunchStartTime { get; set; }
		public TimeSpan LunchEndTime { get; set; }
		public TimeSpan BusinessStartTime { get; set; }
		public TimeSpan BusinessEndTime { get; set; }
	}
}
