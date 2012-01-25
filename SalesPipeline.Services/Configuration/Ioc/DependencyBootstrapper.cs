using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using SocialPipeline.Services.Common.Interfaces;
using SocialPipeline.Services.Common.Repositories;
using SocialPipeline.Services.Interfaces;
using SocialPipeline.Services.Services;
using SocialPipeline.Data.Dal.Interfaces;

namespace SocialPipeline.Services.Configuration.Ioc
{
    public static class DependencyBootstrapper
    {
        public static void Register(IUnityContainer container)
        {
            container.RegisterType<ISessionIdentity, SessionIdentity>(new PerResolveLifetimeManager());

            SocialPipeline.Data.Dal.Configuration.Ioc.DependencyBootstrapper.Register(container);
            
            container.RegisterType<IOrganisationService, OrganisationService>(new PerResolveLifetimeManager(), 
                new InjectionConstructor(typeof(IOrganisationRepository), typeof(IDatabaseLogger)));
            container.RegisterType<IAuthenticationService, AuthenticationService>(new PerResolveLifetimeManager(), 
                new InjectionConstructor(typeof(IUserService), typeof(IDatabaseLogger)));
            container.RegisterType<IUserService, UserService>(new PerResolveLifetimeManager(), 
                new InjectionConstructor(typeof(IUserRepository), typeof(IDatabaseLogger)));
            container.RegisterType<ICompanyService, CompanyService>(new PerResolveLifetimeManager(), 
                new InjectionConstructor(typeof(ICompanyRepository), typeof(IDatabaseLogger)));
            container.RegisterType<IRegistrationService, RegistrationService>(new PerResolveLifetimeManager(), 
                new InjectionConstructor(typeof(IRegistrationRepository), typeof(IUserService), typeof(ICompanyService), typeof(IDatabaseLogger)));
            

        }
    }
}
