using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using SocialPipeline.Services.Interfaces;
using SocialPipeline.Services.Models.Entities.Company;
using SocialPipeline.Services.Models.Entities.User;

namespace SocialPipeline.Services.Contracts
{
    [ContractClassFor(typeof(IRegistrationService))]
    public class RegistrationServiceContract : IRegistrationService
    {
        public int RegisterUser(SocialPipelineUser user, Company company)
        {
            Contract.Requires(!string.IsNullOrEmpty(user.FirstName));
            Contract.Requires(!string.IsNullOrEmpty(user.Email));
            Contract.Requires(!string.IsNullOrEmpty(user.Password));
            Contract.Requires(!string.IsNullOrEmpty(company.Name));

            return default(int);
        }
    }
}
