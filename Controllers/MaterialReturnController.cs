using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace PBL_IF6_Panasonic.Controllers
{
    public class MaterialReturnController : Controller
    {
        // GET: ReturnMaterial/Index
        public IActionResult Index()
        {
            // Hardcoded data for manual entry
            var returnMaterials = new List<dynamic>
            {
                new { No = 1, MaterialName = "Steel", ReturnQuantity = 50, ReasonOfReturn = "Damaged", ReturnStatus = "Approved", ReturnDate = "2024-11-18" },
                new { No = 2, MaterialName = "Copper", ReturnQuantity = 30, ReasonOfReturn = "Overstock", ReturnStatus = "Pending", ReturnDate = "2024-11-19" },
                new { No = 3, MaterialName = "Plastic", ReturnQuantity = 20, ReasonOfReturn = "Wrong Order", ReturnStatus = "Rejected", ReturnDate = "2024-11-20" },
                // Tambahkan data lainnya sesuai kebutuhan
            };

            // Pass the data to the view
            ViewData["ReturnMaterials"] = returnMaterials;
            return View();
        }
    }
}
