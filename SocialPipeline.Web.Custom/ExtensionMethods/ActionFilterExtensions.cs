using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace SocialPipeline.Web.Custom.ExtensionMethods
{
    public static class ActionFilterExtensions
    {
         /// <summary>
        /// Redirects to action.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        /// <param name="responseCode">The response code.</param>
        /// <param name="actionName">Name of the action.</param>
        public static void RedirectToAction(this ActionExecutingContext filterContext, string actionName)
        {
                if (String.IsNullOrEmpty(actionName))
                        throw new ArgumentNullException("actionName");

                RouteValueDictionary values = new RouteValueDictionary();
                values.Add("action", actionName);

                RedirectToAction(filterContext, values);
        }

        /// <summary>
        /// Redirects to action.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        /// <param name="responseCode">The response code.</param>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="controllerName">Name of the controller.</param>
        public static void RedirectToAction(this ActionExecutingContext filterContext, string actionName, string controllerName)
        {
                if (String.IsNullOrEmpty(actionName))
                        throw new ArgumentNullException("actionName");

                if (String.IsNullOrEmpty(controllerName))
                        throw new ArgumentNullException("controllerName");

                RouteValueDictionary values = new RouteValueDictionary();
                values.Add("action", actionName);
                values.Add("controller", controllerName);

                RedirectToAction(filterContext, values);
        }

        /// <summary>
        /// Redirects to action.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        /// <param name="responseCode">The response code.</param>
        /// <param name="values">The values.</param>
        public static void RedirectToAction(this ActionExecutingContext filterContext, RouteValueDictionary values)
        {
                VirtualPathData virtualPath = RouteTable.Routes.GetVirtualPath(filterContext.RequestContext, values);

                string url = null;
                if (virtualPath != null)
                        url = virtualPath.VirtualPath;

                filterContext.HttpContext.Response.Redirect(url);
        }
    }
}
