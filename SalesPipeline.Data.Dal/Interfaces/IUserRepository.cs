using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocialPipeline.Data.Common.Entities;
using SocialPipeline.Data.Common.Exceptions;
using SocialPipeline.Services.Common.Exceptions;

namespace SocialPipeline.Data.Dal.Interfaces
{
    public interface IUserRepository
    {
        /// <exception cref="UnknownUserException">Thrown when the user does not exist</exception>
        /// <exception cref="SocialPipelineDatabaseConnectionException">Thrown when there are issues talking to the database</exception>
        SocialPipelineUserDto FindByEmailAddress(string email);

        /// <exception cref="UnknownUserException">Thrown when the user does not exist</exception>
        /// <exception cref="SocialPipelineDatabaseConnectionException">Thrown when there are issues talking to the database</exception>
        SocialPipelineUserDto FindByUserId(int userId);

        /// <exception cref="UnknownUserException">Thrown when the user does not exist</exception>
        /// <exception cref="SocialPipelineDatabaseConnectionException">Thrown when there are issues talking to the database</exception>
        List<SocialPipelineUserDto> FindAllUsersInCompanyByUserId(int userId); 

        ///<exception cref="UnableToAddUserException">Thrown when there is a sql issue inserting a new user</exception>
        void AddUser(SocialPipelineUserDto userDto);
    }
}
