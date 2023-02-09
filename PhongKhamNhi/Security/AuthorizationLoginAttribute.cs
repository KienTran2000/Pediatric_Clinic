using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace PhongKhamNhi.Security
{
    public class AuthorizationLoginAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session.Contents["user"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Login", action = "Index", Area = "" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
    
}