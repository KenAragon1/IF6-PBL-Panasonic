using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace panasonic.Controllers;

[Authorize]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}