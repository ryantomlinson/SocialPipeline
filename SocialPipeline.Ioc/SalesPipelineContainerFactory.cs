using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace SocialPipeline.Ioc
{
    public class SocialPipelineContainerFactory
    {
        static IUnityContainer container;

        public static IUnityContainer GetUnityContainer()
		{
			if (container != null)
				return container;

			container = new UnityContainer();
			return container;
		}
    }
}
