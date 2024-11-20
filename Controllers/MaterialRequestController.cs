using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace PBL_IF6_Panasonic.Controllers
{
    public class MaterialRequestController : Controller
    {
        
        public IActionResult Index()
        {
           
            var materialRequests = new List<dynamic>
            {
                new { No = 1, RequestedDate = "2024-11-17", RequestedBy = "John Doe", MaterialName = "Copper", Quantity = 10, Weight = 100.0, Remark = "Urgent", Status = "Pending" },
                new { No = 2, RequestedDate = "2024-11-16", RequestedBy = "Jane Smith", MaterialName = "Plastic", Quantity = 20, Weight = 200.0, Remark = "Normal", Status = "Approved" },
                
            };

            
            return View(materialRequests);
        }

       
        public IActionResult Create()
        {
           
            return View();
        }
        public IActionResult Edit(int id)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            
            return RedirectToAction("Index");
        }
    }
}