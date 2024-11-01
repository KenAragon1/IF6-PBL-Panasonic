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
        }

        base.OnActionExecuting(context);
    }



}