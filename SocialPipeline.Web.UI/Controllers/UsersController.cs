using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialPipeline.Services.Common.Exceptions;
using SocialPipeline.Services.Common.Interfaces;
using SocialPipeline.Services.Interfaces;
using SocialPipeline.Services.Models.Entities.User;
using SocialPipeline.Web.ViewModels.Users;

namespace SocialPipeline.Web.UI.Controllers
{
    public class UsersController : SocialPipelineBaseController
    {
        private readonly IUserService userService;

        public UsersController(ISessionIdentity sessionIdentity,
                                IUserService userService) :base (sessionIdentity, userService)
        {
            this.userService = userService;
        }

        public ActionResult Index()
        {
            UsersViewModel model = new UsersViewModel();

            model.Users = userService.FindAllUsersInCompanyByUserId(SocialPipelineUser.Id);

            return View(model);
        }

        public ActionResult Add()
        {
            AddUserViewModel model = new AddUserViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = PopulateUserFromViewModel(model);
                user.CompanyId = SocialPipelineUser.CompanyId;
                try
                {
                    userService.AddUser(user);

                    return RedirectToAction("Index");
                }
                catch (UnableToAddUserException exception)
                {
                    ModelState.AddModelError(string.Empty, "There was an issue registering this new user");
                }
            }

            return View(model);
        }

        private SocialPipelineUser PopulateUserFromViewModel(AddUserViewModel model)
        {
            return new SocialPipelineUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                IsAdmin = model.IsAdmin
            };
        }
    }
}
