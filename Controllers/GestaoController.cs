using Microsoft.AspNetCore.Mvc;

namespace newmarket.Controllers
{
    public class GestaoController : Controller
    {
        public IActionResult Index() {
            return View();
        }   
    }
}