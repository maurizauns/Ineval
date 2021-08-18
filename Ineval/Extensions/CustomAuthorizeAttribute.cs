using Microsoft.AspNet.Identity;
using Ineval.BO;
using System;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Ineval.Extensions
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public string ModuleName { get; set; }
        public string PageName { get; set; }
        public string Action { get; set; }
        public bool NotValidateMenu { get; set; }
        public bool IsPublicAction { get; set; }
        public string AllowActions { get; set; }
        public string PublicActions { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                return false;
            }

            if (IsPublicAction)
            {
                return true;
            }

            var isAuthorized = base.AuthorizeCore(httpContext);
            if (!isAuthorized)
            {
                return false;
            }

            if (NotValidateMenu)
            {
                return true;
            }

            var service = new SwmServices();

            try
            {
                var userId = httpContext.User.Identity.GetUserId();
                var flag = service.UsuarioService.IsAuthorizedPage(userId, ModuleName, Action);

                if (!flag)
                {
                    httpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                }
                return flag;
            }
            finally
            {
                service.Dispose();
            }
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            NotValidateMenu = AllowActions != null && AllowActions.Contains(filterContext.ActionDescriptor.ActionName);
            IsPublicAction = PublicActions != null && PublicActions.Contains(filterContext.ActionDescriptor.ActionName);

            base.OnAuthorization(filterContext);
            if (filterContext.HttpContext.Response.StatusCode == (int)HttpStatusCode.Forbidden)
            {
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            //var sessionPerson = filterContext.HttpContext.Session["personId"];
            //if (UserSession.EmpresaId  == Guid.Empty)
            //{
            //    filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            //}

            base.OnAuthorization(filterContext);
        }
    }
}