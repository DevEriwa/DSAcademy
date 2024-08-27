using Microsoft.AspNetCore.Mvc;

namespace DSAcademy.Controllers
{
	public class NotificationController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
