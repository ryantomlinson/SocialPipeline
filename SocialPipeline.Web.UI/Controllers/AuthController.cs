using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SocialPipeline.Services.Common.Exceptions;
using SocialPipeline.Services.Common.Interfaces;
using SocialPipeline.Services.Interfaces;
using SocialPipeline.Web.ViewModels.Logon;

namespace SocialPipeline.Web.UI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthenticationService authenticationService;

        public AuthController(IAuthenticationService authenticationService,
                                ISessionIdentity sessionIdentity,
                                IUserService userService) 
        {
            this.authenticationService = authenticationService;
        }

        public ActionResult LogOn()
        {
            LogonViewModel model = new LogonViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult LogOn(LogonViewModel logonViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = authenticationService.LogonUser(logonViewModel.Email, logonViewModel.Password);

                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, logonViewModel.Email, DateTime.Now,
                                                    DateTime.Now.AddDays(2), logonViewModel.RememberMe, user.Id.ToString(CultureInfo.InvariantCulture));

                    string encTicket = FormsAuthentication.Encrypt(ticket);
                    Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket) { Expires = DateTime.Now.AddDays(2) });

                    return RedirectToAction("Index", "Pipeline");
                }
                catch (AuthenticationException exception)
                {
                    
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
                catch (LogonException exception)
                {
                    
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }    
                catch (Exception exception)
                {
                    
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(logonViewModel);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("LogOn");
        }

    }
}
