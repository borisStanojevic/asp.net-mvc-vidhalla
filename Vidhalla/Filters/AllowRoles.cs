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
        private readonly Role[] _roles;

        public AllowRoles(params Role[] roles)
        {
            _roles = roles ?? new Role[] { };
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Ako nije autentikovan redirektuj na login
            if (HttpContext.Current.Session["AccountInSession"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary()
                {
                    {"Controller", "Accounts"},
                    {"Action", "Login"}
                });
            }
            //U suprotnom postoji korisnik u sesiji pa...
            else
            {
                //Ako je eksplicitno definisano kojim je to rolama dozvoljeno izvrsavanje akcije, odnosno
                //ako je kao parametar action filter atributa proslijedjen niz tipa Role
                //onda provjeri da li ulogovani korisnik ima neku od tih rola
                //Ako nema vrati forbidden
                if (_roles.Length > 0)
                {
                    var accountInSessionRole =
                        ((AccountSessionModel)HttpContext.Current.Session["AccountInSession"]).Role;
                    var hasRequestedRole = _roles.Any(role => role == accountInSessionRole);
                    if (hasRequestedRole == false)
                        filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }

                //Ako nisu proslijedjene role smatram da je svim rolama dozvoljeno izvrsavanje akcije
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
