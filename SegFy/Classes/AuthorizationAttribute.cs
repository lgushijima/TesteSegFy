using System;
using System.Web;
using System.Web.Mvc;

namespace SegFy.Classes
{
    public class AuthorizationAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var ssn = CookiesManagement.Get(Resources.Resource.UserCookieName);
            if (ssn==null)
            {
                return false;
            }

            return true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            bool isAjaxCall = string.Equals("XMLHttpRequest", filterContext.HttpContext.Request.Headers["x-requested-with"], StringComparison.OrdinalIgnoreCase);
            if (isAjaxCall)
            {
                Result ret = new Result();
                ret.setError(Resources.Resource.SessaoExpiradaOuAcessoNegado);

                filterContext.Result = new JsonResult
                {
                    Data = ret,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}