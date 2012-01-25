using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocialPipeline.Data.Common.Entities;
using SocialPipeline.Data.Common.Exceptions;

namespace SocialPipeline.Data.Dal.Interfaces
{
    public interface IRegistrationRepository
    {
        /// <exception cref="RegistrationException">Thrown when there are issues registering the user</exception>
        /// <exception cref="SocialPipelineDatabaseConnectionException">Thrown when there are database issues</exception>
        int RegisterUser(SocialPipelineUserDto userDto, CompanyDto companyDto);
    }
}
