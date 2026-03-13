using Microsoft.AspNetCore.Mvc;

namespace FiscalFlow.API.Controllers
{
    public class DocumentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
