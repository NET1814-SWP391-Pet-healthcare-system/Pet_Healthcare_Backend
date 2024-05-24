using Microsoft.AspNetCore.Mvc;

namespace PetHealthCareSystem_BackEnd.Controllers
{
    public class AppointmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
