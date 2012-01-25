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
using SocialPipeline.Services.Models.Entities.Company;
using SocialPipeline.Services.Models.Entities.User;
using SocialPipeline.Web.ViewModels.Registration;

namespace SocialPipeline.Web.UI.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IRegistrationService registrationService;

        public RegistrationController(IRegistrationService registrationService,
                                        ISessionIdentity sessionIdentity,
                                        IUserService userService) 
        {
            this.registrationService = registrationService;
        }

        public ActionResult Index()
        {
            RegistrationViewModel model = new RegistrationViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
			{
                var company = GetCompanyFromViewModel(model);
                var user = GetUserFromViewModel(model);
                try
                {
                    user.Id = registrationService.RegisterUser(user, company);
                    FormsAuthentication.SetAuthCookie(user.FirstName.ToString(CultureInfo.InvariantCulture), false);
                    return RedirectToAction("Index", "Pipeline");
                }
                catch (UserAlreadyRegisteredException exception)
                {
                    ModelState.AddModelError("Email", "Email is already registered with SocialPipeline.");
                }
                catch (CompanyAlreadyExistsException exception)
                {
                    ModelState.AddModelError("CompanyName", "Company name is already registered with SocialPipeline.");
                }
            }
            return View(model);
        }

        private SocialPipelineUser GetUserFromViewModel(RegistrationViewModel model)
        {
            var user = new SocialPipelineUser
                           {
                               FirstName = model.FirstName,
                               LastName = model.LastName,
                               Email = model.Email,
                               IsAdmin = true,
                               Password = model.Password
                           };
            return user;
        }

        private Company GetCompanyFromViewModel(RegistrationViewModel model)
        {
            return new Company
                              {
                                  Name = model.CompanyName
                              };
        }
    }
}
