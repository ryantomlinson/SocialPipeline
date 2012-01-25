using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using SocialPipeline.Services.Interfaces;
using SocialPipeline.Services.Models.Entities.User;

namespace SocialPipeline.Services.Contracts
{
    [ContractClassFor(typeof(IAuthenticationService))]
    public class AuthenticationServiceContract : IAuthenticationService
    {
        public SocialPipelineUser LogonUser(string email, string password)
        {
            Contract.Requires(!string.IsNullOrEmpty(email));
            Contract.Requires(!string.IsNullOrEmpty(password));

            return default(SocialPipelineUser);
        }
    }
}
