using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace PBL_IF6_Panasonic.Controllers
{
    public class SaveMaterialReturnController : Controller
    {
        
        public IActionResult Index()
        {
         
            var returnMaterials = new List<dynamic>
            {
                new { MaterialName = "Steel", ReturnQuantity = 50, ReasonOfReturn = "Damaged", ReturnDate = "2024-11-18" },
                new { MaterialName = "Copper", ReturnQuantity = 30, ReasonOfReturn = "Overstock", ReturnDate = "2024-11-19" },
                new { MaterialName = "Plastic", ReturnQuantity = 20, ReasonOfReturn = "Wrong Order", ReturnDate = "2024-11-20" },
              
            };

            ViewData["ReturnMaterials"] = returnMaterials;

            return View();
        }
    }
}
