using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace PBL_IF6_Panasonic.Controllers
{
    public class PrepRoomController : BaseController
    {
        public IActionResult Index()
        {
            var materials = new List<dynamic>
            {
                new { 
                    Id = 1, 
                    Name = "Material A", 
                    PartNumber = "A001", 
                    Weight = 10, 
                    Unit = "Spool", 
                    DetailUnit ="1 Spool = 100kg",
                    Quantity = "30 Kg", 
                    ImageUrl = "hhttps://www.google.com/imgres?q=howls&imgurl=https%3A%2F%2Fcdn.idntimes.com%2Fcontent-images%2Fcommunity%2F2023%2F02%2Fpolish-20230208-104932231-b38ff29b9dde05cc364fe81f74650dd5-7a9ccbd76b9c817faaf47caf65da8570.png&imgrefurl=https%3A%2F%2Fwww.idntimes.com%2Fhype%2Fentertainment%2Fervina-emma-wardani%2Fpesan-moral-anime-howls-moving-castle-c1c2&docid=7j0iwnLPQjP9AM&tbnid=AosTZ7vS3Q12MM&vet=12ahUKEwjb0PTZw-OJAxU9UGcHHWX7FvkQM3oECFAQAA..i&w=770&h=514&hcb=2&ved=2ahUKEwjb0PTZw-OJAxU9UGcHHWX7FvkQM3oECFAQAA"
                },
                new { 
                    Id = 2, 
                    Name = "Material B", 
                    PartNumber = "B002", 
                    Unit = "Spool",
                    DetailUnit = "1 Spool = 100Kg",
                    Quantity = "20 Kg", 
                    ImageUrl = "hhttps://www.google.com/imgres?q=howls&imgurl=https%3A%2F%2Fcdn.idntimes.com%2Fcontent-images%2Fcommunity%2F2023%2F02%2Fpolish-20230208-104932231-b38ff29b9dde05cc364fe81f74650dd5-7a9ccbd76b9c817faaf47caf65da8570.png&imgrefurl=https%3A%2F%2Fwww.idntimes.com%2Fhype%2Fentertainment%2Fervina-emma-wardani%2Fpesan-moral-anime-howls-moving-castle-c1c2&docid=7j0iwnLPQjP9AM&tbnid=AosTZ7vS3Q12MM&vet=12ahUKEwjb0PTZw-OJAxU9UGcHHWX7FvkQM3oECFAQAA..i&w=770&h=514&hcb=2&ved=2ahUKEwjb0PTZw-OJAxU9UGcHHWX7FvkQM3oECFAQAA"
                },
                new { 
                    Id = 3, 
                    Name = "Material C", 
                    PartNumber = "C003", 
                    Unit = "Spool", 
                    DetailUnit = "1 Spool = 100kg",
                    Quantity = "35 Kg", 
                    ImageUrl = "https://www.google.com/imgres?q=howls&imgurl=https%3A%2F%2Fcdn.idntimes.com%2Fcontent-images%2Fcommunity%2F2023%2F02%2Fpolish-20230208-104932231-b38ff29b9dde05cc364fe81f74650dd5-7a9ccbd76b9c817faaf47caf65da8570.png&imgrefurl=https%3A%2F%2Fwww.idntimes.com%2Fhype%2Fentertainment%2Fervina-emma-wardani%2Fpesan-moral-anime-howls-moving-castle-c1c2&docid=7j0iwnLPQjP9AM&tbnid=AosTZ7vS3Q12MM&vet=12ahUKEwjb0PTZw-OJAxU9UGcHHWX7FvkQM3oECFAQAA..i&w=770&h=514&hcb=2&ved=2ahUKEwjb0PTZw-OJAxU9UGcHHWX7FvkQM3oECFAQAA"
                }
            };

            ViewData["Materials"] = materials;

            return View("Data");
        }
    }
}
