using Microsoft.AspNetCore.Mvc;

namespace NotificationService.WebAPI.DemoApp
{
    
    [Route("demoapp")]
    public class DemoAppController : Controller
    {
        public IActionResult Index()
        {
            return View("../../DemoApp/Index");
        }
    }
}
