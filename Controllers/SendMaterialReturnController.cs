using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace PBL_IF6_Panasonic.Controllers
{
    public class SendMaterialReturnController : Controller
    {
        public IActionResult Index()
        {
           
            var materialReturns = new List<dynamic>
            {
                new { No = 1, MaterialName = "Steel", ReturnQuantity = 15, Reason = "Damaged", Status = "Pending" },
                new { No = 2, MaterialName = "Copper", ReturnQuantity = 10, Reason = "Excess", Status = "Approved" },
                new { No = 3, MaterialName = "Plastic", ReturnQuantity = 5, Reason = "Defective", Status = "Pending" }
            };

            
            return View(materialReturns);
        }

        [HttpPost]
        public IActionResult UpdateStatus(int id, string status)
        {
            
            return Json(new { success = true, newStatus = status });
        }
    }
}
