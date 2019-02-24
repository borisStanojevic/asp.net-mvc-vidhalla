using System.Web;
using Vidhalla.Core.Domain;

namespace Vidhalla.Extensions
{
    public static class SessionExtension
    {
        public static void SetAccount(this HttpSessionStateBase session, AccountSessionModel accountSessionModel)
        {
            HttpContext.Current.Session["AccountInSession"] = accountSessionModel;
        }

        public static AccountSessionModel GetAccount(this HttpSessionStateBase session)
        {
            return (AccountSessionModel)HttpContext.Current.Session["AccountInSession"];
        }

        public static void Clear(this HttpSessionStateBase session)
        {
            session["AccountInSesssion"] = null;
        }
    }
}