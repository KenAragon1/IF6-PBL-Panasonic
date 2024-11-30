using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace PBL_IF6_Panasonic.Controllers
{
    public class MaterialUsageController : BaseController
    {
        
        public IActionResult Index()
        {
           
            var materialUsages = new List<dynamic>
            {
                new { No = 1, RecipientName = "Aulia",  PartNumber ="A1002", MaterialName = "Copper",  Quantity = "10 Kg",  DetailUnit="1 Spool = 100kg ", Unit = "Spool", Line = "Line 1" },
                new { No = 2, RecipientName = "Sabrina", PartNumber ="A1003", MaterialName = "Plastic",  Quantity = "20 Kg" , DetailUnit="1 Spool = 100kg ",  Unit = "Spool", Line = "Line 2" },
                new { No = 3, RecipientName = "Aulia Sabrina",  PartNumber ="A1004", MaterialName = "Steel", Quantity = "50 Kg" , DetailUnit="1 Spool = 100kg ",  Unit = "Spool", Line = "Line 3" },
          
            };

           
            ViewData["MaterialUsages"] = materialUsages;
            return View();
        }
    }
}
