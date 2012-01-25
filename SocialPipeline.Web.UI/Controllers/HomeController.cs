using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialPipeline.Services.Common.Interfaces;
using SocialPipeline.Services.Interfaces;

namespace SocialPipeline.Web.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOrganisationService organisationService;

        public HomeController(IOrganisationService organisationService,
                                ISessionIdentity sessionIdentity,
                                IUserService userService) 
        {
            this.organisationService = organisationService;
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
