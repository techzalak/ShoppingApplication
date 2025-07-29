using Microsoft.AspNetCore.Mvc;

namespace ZalakProject.Controllers
{
    public class AdminProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
