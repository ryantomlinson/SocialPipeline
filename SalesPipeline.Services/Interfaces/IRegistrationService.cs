using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using SocialPipeline.Services.Common.Exceptions;
using SocialPipeline.Services.Contracts;
using SocialPipeline.Services.Models.Entities.Company;
using SocialPipeline.Services.Models.Entities.User;

namespace SocialPipeline.Services.Interfaces
{
    [ContractClass(typeof(RegistrationServiceContract))]
    public interface IRegistrationService
    {
        /// <exception cref="UnableToRegisterUserException">Thrown when there are issues registering a new user.</exception>
        int RegisterUser(SocialPipelineUser user, Company company);
    }
}
