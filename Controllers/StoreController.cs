using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace panasonic.Controllers;

[Authorize]
public class StoreController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Material()
    {
        return View();
    }
}