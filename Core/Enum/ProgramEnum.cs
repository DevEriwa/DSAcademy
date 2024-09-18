using System.ComponentModel;

namespace Core.Enum
{
	public enum ProgramEnum
	{
		[Description("Frontend Developer")]
		Frontend = 1,
		[Description("Backend Developer")]
		Backend = 2,
		[Description("Full-Stack Developer")]
		FullStack = 3,
		[Description("Digital Marketing")]
		DigitalMarketing = 4,
		[Description("Social Media Marketing")]
		SocialMediaMarketing  = 5,
		[Description("Website Disign")]
		WebsiteDisign = 5,
	}
	public enum CompanySettings
	{
		[Description("DOC Booking Module")]
		DOCBookingModule,
		[Description("Store Module")]
		StoreModule = 3,
		[Description("Treatment Module")]
		TreatmentModule = 4,
		[Description("Pet Boarding Module")]
		PetBoardingModule = 5,
		[Description("Is MOT reminder enabled")]
		IsMOTReminderEnabled = 6,
		[Description("enabled reminder")]
		IsReminderEnabled = 7,
		[Description("Enabled receipt waterMark")]
		IsReceiptWaterMark = 8,
		[Description("Enabled Price Mark-Up")]
		IsPriceMarkUp = 9,
		[Description("Disable VAT For Quick Visit")]
		DisableVATForQuickVisit = 10,
		[Description("Enable Mark VHC Parts Green")]
		MarkVHCPartsGreen = 11,
		[Description("Don't Pull Customer Detail When Fetching Car")]
		DontPullCustomerDetailWhenFetchingCar = 12
	}
}
