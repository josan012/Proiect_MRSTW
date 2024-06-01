using System;
using System.Web.Mvc;

namespace handmadeShop.Web.Filters
{
    public class RedirectToRegister : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated &&
                !String.Equals(filterContext.ActionDescriptor.ActionName, "Register", StringComparison.OrdinalIgnoreCase) &&
                !String.Equals(filterContext.ActionDescriptor.ControllerDescriptor.ControllerName, "Account", StringComparison.OrdinalIgnoreCase))
            {
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary {
                        {"controller", "Account"},
                        {"action", "Register"}
                    }
                );
            }
            base.OnActionExecuting(filterContext);
        }
    }
}