using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;

namespace SocialPipeline.Web.UI.Factories
{
    public class SocialPipelineControllerFactory : DefaultControllerFactory
	{
		IUnityContainer Container;

		public SocialPipelineControllerFactory(IUnityContainer container)
		{
			this.Container = container;
		}

		protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
		{
			
			try {
            if (controllerType == null)
                throw new ArgumentNullException("controllerType");

            if (!typeof(IController).IsAssignableFrom(controllerType))
                throw new ArgumentException(string.Format(
                    "Type requested is not a controller: {0}",
                    controllerType.Name),
                    "controllerType");
            return Container.Resolve(controllerType) as IController;
        }
        catch { return null; }
			
		}
	}
}
