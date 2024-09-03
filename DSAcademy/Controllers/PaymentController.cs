using Microsoft.AspNetCore.Mvc;

namespace DSAcademy.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
