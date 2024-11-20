using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace PBL_IF6_Panasonic.Controllers
{
    public class MaterialUsageController : Controller
    {
        
        public IActionResult Index()
        {
           
            var materialUsages = new List<dynamic>
            {
                new { No = 1, RecipientName = "Aulia", EmployeeId = "EMP123", MaterialName = "Copper", Weight = 100.0, Quantity = 10, Unit = "kg", Line = "Line 1" },
                new { No = 2, RecipientName = "Sabrina", EmployeeId = "EMP124", MaterialName = "Plastic", Weight = 200.0, Quantity = 20, Unit = "kg", Line = "Line 2" },
                new { No = 3, RecipientName = "Aulia Sabrina", EmployeeId = "EMP125", MaterialName = "Steel", Weight = 500.0, Quantity = 50, Unit = "kg", Line = "Line 3" },
          
            };

           
            ViewData["MaterialUsages"] = materialUsages;
            return View();
        }
    }
}
