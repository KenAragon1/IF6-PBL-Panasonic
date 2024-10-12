using Microsoft.AspNetCore.Mvc;
using panasonic.Models;

namespace panasonic.Controllers;

public class MaterialController : Controller
{
    public IActionResult Index()
    {
        // fake data
        var materials = new List<Material>{
            new Material{Id =1, Name = "Tembaga", TotalQuantity = "200 pcs", TotalWeight = "200 kg"},
            new Material{Id =2, Name = "Silicon", TotalQuantity = "200 pcs", TotalWeight = "200 kg"},
            new Material{Id =3, Name = "Hello", TotalQuantity = "200 pcs", TotalWeight = "200 kg"},
            new Material{Id =4, Name = "Test", TotalQuantity = "200 pcs", TotalWeight = "200 kg"},
        };
        return View(materials);
    }

    public IActionResult Storage(int id)
    {

        return View();
    }
}