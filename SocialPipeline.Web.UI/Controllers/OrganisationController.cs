using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialPipeline.Services.Common.Exceptions;
using SocialPipeline.Services.Common.Interfaces;
using SocialPipeline.Services.Interfaces;
using SocialPipeline.Services.Models.Entities.Organisation;
using SocialPipeline.Services.Models.Helpers;
using SocialPipeline.Web.Custom.ExtensionMethods;
using SocialPipeline.Web.ViewModels.Organisation;
using SocialPipeline.Web.ViewModels.Organisations;

namespace SocialPipeline.Web.UI.Controllers
{
    public class OrganisationController : SocialPipelineBaseController
    {
        private readonly IOrganisationService organisationService;

        public OrganisationController(IOrganisationService organisationService,
                                        ISessionIdentity sessionIdentity,
                                        IUserService userService) : base(sessionIdentity, userService)
        {
            this.organisationService = organisationService;
        }

        public ActionResult Index()
        {
            OrganisationsViewModel model = new OrganisationsViewModel();

            model.Organisations = organisationService.GetOrganisationsByCompanyId(SocialPipelineUser.CompanyId);

            return View(model);
        }

        [HttpGet]
        public JsonResult Add()
        {
            AddOrganisationViewModel model = new AddOrganisationViewModel();

            return Json(new
            {
                Html = this.RenderPartialView("_Add", model)
            }, JsonRequestBehavior.AllowGet);
        } 

        [HttpPost]
        public JsonResult Add(AddOrganisationViewModel model)
        {
            string message = string.Empty;
            OrganisationsViewModel orgModel = new OrganisationsViewModel();
            try
            {
                var organisation = PopulateOrganisationFromViewModel(model);
                organisationService.AddOrganisation(organisation);

                orgModel.Organisations = organisationService.GetOrganisationsByCompanyId(SocialPipelineUser.CompanyId);
            }
            catch (UnableToAddOrganisationException exception)
            {
                message = "There was an issue registering this new organisation";
            }

            return Json(new
            {
                Html = this.RenderPartialView("_Organisations", orgModel),
                Message = message
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int id, OrganisationAssociation? tab)
        {
            OrganisationDetailsViewModel model = new OrganisationDetailsViewModel();

            var organisation = organisationService.GetOrganisation(id);
            model.Organisation = organisation;

            if (tab != null)
            {
                if (tab == OrganisationAssociation.People)
                {
                    model.HasPeople = true;
                }
            }

            return View(model);
        }

        private Organisation PopulateOrganisationFromViewModel(AddOrganisationViewModel model)
        {
            return new Organisation()
                       {
                           Active = true,
                           Name = model.Name,
                           CreatedBy = SocialPipelineUser
                       };
        }
    }
}
