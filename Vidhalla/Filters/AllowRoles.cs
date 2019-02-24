using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Vidhalla.Core.Domain;

namespace Vidhalla.Filters
{
    public class AllowRoles : ActionFilterAttribute, IActionFilter
    {
        private readonly IEnumerable<Role> _roles;

        public AllowRoles(params Role[] roles)
        {
            if (roles.Length == 0)
                throw new ArgumentException();

            _roles = new List<Role>(roles.Length);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["AccountInSession"] == null)
            {

                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary()
                {
                    {"Controller", "Accounts"},
                    {"Action", "Login"}
                });
            }
            if (HttpContext.Current.Session["AccountInSession"] != null)
            {
                var accountInSessionRole =
                    ((AccountSessionModel)HttpContext.Current.Session["AccountInSession"]).Role;
                var hasRequestedRole = false;
                foreach (var role in _roles)
                {
                    if (!role.Equals(accountInSessionRole))
                        continue;
                    hasRequestedRole = true;
                }
                if (hasRequestedRole == false)
                {
                    filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
