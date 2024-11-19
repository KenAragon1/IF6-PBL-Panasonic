using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


public class BaseController : Controller
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (User.Identity.IsAuthenticated)
        {
            var username = User.Identity.Name;

            ViewBag.Username = username;
            ViewBag.Role = User.FindFirst(ClaimTypes.Role)?.Value;
            ViewBag.AreaId = User.FindFirst("AreaId")?.Value;
        }

        base.OnActionExecuting(context);
    }



}