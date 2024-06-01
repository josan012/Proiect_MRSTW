﻿using System.Web;
using System.Web.Mvc;

namespace handmadeShop.Web.Filters
{
    public class SessionTimeout : ActionFilterAttribute
    {
      
        public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
           

            HttpSessionStateBase session = filterContext.HttpContext.Session;

            if (session == null || session.IsNewSession)
            {
               
                filterContext.Result = new RedirectResult("~/Account/Logout");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}