using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectPublish.ActionFilters
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MyControllerAuthorizeAttribute : AuthorizeAttribute
    {
        private string[] _rolesArray;
        private string[] _rolesAll;
        private string _authorizationGroupName;

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            if (controllerName.ToUpper() != "ADMIN")
            {
                this.Roles = "Admin, " + controllerName;
            }
            else
            {
                this.Roles = "Admin";
            }
            base.OnAuthorization(filterContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new
            {
                Controller = "AccessDenied",
                Action = "Index"
            }));
        }

    }
}