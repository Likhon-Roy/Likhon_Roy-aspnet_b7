using Microsoft.AspNetCore.Mvc;

namespace Hospital.Areas.Patient.Controllers
{
    [Area("Identity")]
    public class IdentityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
