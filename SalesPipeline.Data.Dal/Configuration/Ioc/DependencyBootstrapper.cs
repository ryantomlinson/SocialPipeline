using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using SocialPipeline.Data.Dal.Interfaces;
using SocialPipeline.Data.Dal.Repositories;
using SocialPipeline.Services.Common.Interfaces;
using SocialPipeline.Services.Common.Repositories;

namespace SocialPipeline.Data.Dal.Configuration.Ioc
{
    public static class DependencyBootstrapper
    {
        public static void Register(IUnityContainer container)
        {
            container.RegisterType<IDatabaseLogger, DatabaseLogger>(new PerResolveLifetimeManager(),
                new InjectionConstructor(typeof(ISessionIdentity)));
            container.RegisterType<IOrganisationRepository, OrganisationRepository>(new PerResolveLifetimeManager(),
                new InjectionConstructor(typeof(IDatabaseLogger)));
            container.RegisterType<IUserRepository, UserRepository>(new PerResolveLifetimeManager(),
                new InjectionConstructor(typeof(IDatabaseLogger)));
            container.RegisterType<ICompanyRepository, CompanyRepository>(new PerResolveLifetimeManager(),
                new InjectionConstructor(typeof(IDatabaseLogger)));
            container.RegisterType<IRegistrationRepository, RegistrationRepository>(new PerResolveLifetimeManager());
        }
    }
}
