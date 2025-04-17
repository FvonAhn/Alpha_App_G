using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class UpdateController : Controller
    {
        public IActionResult UpdateProject()
        {
            return View();
        }

        public IActionResult UpdateUser() 
        {
            return View();
        }
    }
}
