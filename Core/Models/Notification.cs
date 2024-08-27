using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
	public class Notification
	{
		[Key]
		public int Id { get; set; }
		[Display(Name = "Access Token")]
		public string? FacebookAccessToken { get; set; }
		[Display(Name = "App Id")]
		public string? FacebookAppId { get; set; }
		[Display(Name = "App Secret Key")]
		public string? FacebookSecretKey { get; set; }
		[Display(Name = "Google Map Id")]
		public string? GoogleMapId { get; set; }
		[Display(Name = "Enable Price Mark-Up")]
		public bool IsPriceMarkUp { get; set; }
		public int? PageSizeData { get; set; }
		public int? CompanyWorkHourPerDay { get; set; }
		public string? CompanyInvoiceTerms { get; set; }
		public Guid? CompanyId { get; set; }
		[ForeignKey("CompanyId")]
		public virtual Company? Company { get; set; }
		public bool IsReceiptWaterMark { get; set; }
		public long? PriceMarkUp { get; set; }
		public string? QRScanDetails { get; set; }
		public string? QRScanLink { get; set; }
		public decimal? HourlyRate { get; set; }
		public string? ReceiptColour { get; set; }
		public string? ReceiptSubColour { get; set; }
		public TimeSpan LunchStartTime { get; set; }
		public TimeSpan LunchEndTime { get; set; }
		public TimeSpan BusinessStartTime { get; set; }
		public TimeSpan BusinessEndTime { get; set; }
	}
}
