using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using SocialPipeline.Services.Common.Exceptions;
using SocialPipeline.Web.UI.Controllers;
using SocialPipeline.Web.Custom.ExtensionMethods;

namespace SocialPipeline.Web.UI.ActionFilters
{
    public class PopulateUserAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			base.OnActionExecuting(filterContext);

            SocialPipelineBaseController controller			= (SocialPipelineBaseController)filterContext.Controller;
            
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
            {
                filterContext.RedirectToAction("LogOff", "Auth");
            }
                

            FormsAuthenticationTicket authTicket =       FormsAuthentication.Decrypt(authCookie.Value);
            int userId = -1; 
            Int32.TryParse(authTicket.UserData, out userId);
            if (userId < 1)
            {
                filterContext.RedirectToAction("LogOff", "Auth");
            }

            try
            {
                PopulateSessionIdentity(controller, userId);
                controller.SocialPipelineUser = controller.UserService.FindByUserId(userId);
            }
            catch (FindUserException exception)
            {
                //log
                throw;
            }

        }

        private void PopulateSessionIdentity(SocialPipelineBaseController controller, int userId)
        {
            controller.SessionIdentity.UserId = userId;
            controller.SessionIdentity.IpAddress = controller.HttpContext.Request.UserHostAddress;
        }

    }
}