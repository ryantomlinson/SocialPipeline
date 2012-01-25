using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using SocialPipeline.Services.Common.Exceptions;
using SocialPipeline.Services.Contracts;
using SocialPipeline.Services.Models.Entities.User;

namespace SocialPipeline.Services.Interfaces
{
    [ContractClass(typeof(AuthenticationServiceContract))]
    public interface IAuthenticationService
    {
        /// <exception cref="AuthenticationException">Thrown when the user does not exist.</exception>
        /// <exception cref="LogonException">Thrown when there are issues logging in not related to finding the user.</exception>
        SocialPipelineUser LogonUser(string email, string password);
    }
}
