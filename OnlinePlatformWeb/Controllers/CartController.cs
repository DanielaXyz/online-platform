using Microsoft.AspNetCore.Mvc;

namespace OnlinePlatformWeb.Controllers;

[ApiController]
[Route("[controller]")]
public class CartController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
