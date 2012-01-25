using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;

namespace SocialPipeline.Web.UI.Factories
{
    public class SocialPipelineControllerActivator : IControllerActivator
    {
        private IUnityContainer container;
        public SocialPipelineControllerActivator(IUnityContainer container)
        {
         this.container = container;   
        }
        public IController Create(RequestContext requestContext, Type controllerType)
        {
            return container.Resolve(controllerType) as IController;
        }
    }
}