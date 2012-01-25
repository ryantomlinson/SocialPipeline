using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialPipeline.Services.Common.Interfaces;
using SocialPipeline.Services.Interfaces;
using SocialPipeline.Services.Models.Entities.User;
using SocialPipeline.Web.UI.ActionFilters;

namespace SocialPipeline.Web.UI.Controllers
{
    [PopulateUser]
    public class SocialPipelineBaseController : Controller
    {
        public SocialPipelineUser       SocialPipelineUser { get; set; }
        public ISessionIdentity			SessionIdentity;
        public IUserService             UserService;

        public SocialPipelineBaseController(ISessionIdentity sessionIdentity, IUserService userService)
        {
            this.SessionIdentity = sessionIdentity;
            this.UserService = userService;
        }
    }
}
