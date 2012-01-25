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
    [ContractClass(typeof(UserServiceContract))]
    public interface IUserService
    {
        /// <exception cref="FindUserException">Thrown when there are issues finding the user of the user does not exist.</exception>
        SocialPipelineUser FindByEmailAddress(string email);

        /// <exception cref="FindUserException">Thrown when there are issues finding the user of the user does not exist.</exception>
        SocialPipelineUser FindByUserId(int userId);

        /// <exception cref="FindUserException">Thrown when there are issues finding the user of the user does not exist.</exception>
        List<SocialPipelineUser> FindAllUsersInCompanyByUserId(int userId); 

        /// <exception cref="UnableToAddUserException">Thrown when there is an issue adding a new user</exception>
        void AddUser(SocialPipelineUser user);
    }
}
