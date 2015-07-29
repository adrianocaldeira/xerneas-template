using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Template.Repository;
using Thunder.Web;

namespace Template.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class AuthorizationAttribute : AuthorizeAttribute
    {
        public string IgnoreActions { get; set; }

        /// <summary>
        /// When overridden, provides an entry point for custom authorization checks.
        /// </summary>
        /// <returns>
        /// true if the user is authorized; otherwise, false.
        /// </returns>
        /// <param name="httpContext">The HTTP context, which encapsulates all HTTP-specific information about an individual HTTP request.</param><exception cref="T:System.ArgumentNullException">The <paramref name="httpContext"/> parameter is null.</exception>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var user = httpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                return false;
            }

            return IsIgnore(httpContext) || AllowAccess(httpContext);
        }

        /// <summary>
        /// Processes HTTP requests that fail authorization.
        /// </summary>
        /// <param name="filterContext">Encapsulates the information for using <see cref="T:System.Web.Mvc.AuthorizeAttribute"/>. The <paramref name="filterContext"/> object contains the controller, HTTP context, request context, action result, and route data.</param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var request = filterContext.RequestContext.HttpContext.Request;

            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                const string unauthorizedMessage = "Você não possui permissão de acesso à esta funcionalidade!";

                filterContext.RequestContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                if (request["X-IFRAME"] == "true")
                {
                    filterContext.Result = new ContentResult
                    {
                        Content =
                            "<script>if(window.parent){window.parent.$.thunder.alert(\"" + unauthorizedMessage +
                            "\",{type: \"warning\"});}</script>"
                    };
                }
                else
                {
                    filterContext.Result = new HttpStatusCodeResult((int)HttpStatusCode.Forbidden, unauthorizedMessage);
                }
            }
            else
            {
                if (request.IsAjaxRequest())
                {
                    var loginUrl = FormsAuthentication.LoginUrl;

                    if (!string.IsNullOrEmpty(request.Headers.Get("Url-Parent")))
                    {
                        loginUrl = string.Concat(loginUrl, "?ReturnUrl=", request.Headers.Get("Url-Parent"));
                    }

                    filterContext.RequestContext.HttpContext.Response.AddHeader("Unauthorized-Url", loginUrl);
                    filterContext.Result = new JsonResult() {JsonRequestBehavior = JsonRequestBehavior.AllowGet};
                }
                else
                {
                    base.HandleUnauthorizedRequest(filterContext);
                }
            }
        }

        private static bool AllowAccess(HttpContextBase httpContext)
        {
            var routeData = httpContext.Request.RequestContext.RouteData;
            var controllerName = routeData.GetRequiredString("controller");
            var actionName = routeData.GetRequiredString("action");
            var httpCookie = httpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
            var authenticationTicket = FormsAuthentication.Decrypt(httpCookie.Value);
            var userData = authenticationTicket.UserData.Json<XerneasUserData>();
            var allowAccess = new UserRepository().AllowAccess(userData.Id, controllerName.ToLower(),
                actionName.ToLower(), httpContext.Request.HttpMethod.ToLower());

            return allowAccess;
        }

        private bool IsIgnore(HttpContextBase httpContext)
        {
            if (string.IsNullOrWhiteSpace(IgnoreActions)) return false;

            var routeData = httpContext.Request.RequestContext.RouteData;
            var actionName = routeData.GetRequiredString("action");
            var actions = IgnoreActions.ToLower().Split(',').ToList();
            
            return actions.Contains(actionName.ToLower());
        }
    }
}