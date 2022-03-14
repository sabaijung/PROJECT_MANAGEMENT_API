using Microsoft.AspNetCore.Mvc;

namespace PJM.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
