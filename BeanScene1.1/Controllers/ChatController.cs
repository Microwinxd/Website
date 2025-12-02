using Microsoft.AspNetCore.Mvc;

namespace BeanScene1._1.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
