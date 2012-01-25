using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialPipeline.Services.Common.Interfaces;
using SocialPipeline.Services.Interfaces;

namespace SocialPipeline.Web.UI.Controllers
{
    [Authorize]
    public class PipelineController : SocialPipelineBaseController
    {
        public PipelineController(ISessionIdentity sessionIdentity,
                                IUserService userService) :base (sessionIdentity, userService)
        {
            
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
