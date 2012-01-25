using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using SocialPipeline.Services.Interfaces;
using SocialPipeline.Services.Models.Entities.User;

namespace SocialPipeline.Services.Contracts
{
    [ContractClassFor(typeof(IUserService))]
    public class UserServiceContract : IUserService
    {
        public SocialPipelineUser FindByEmailAddress(string email)
        {
            Contract.Requires(!string.IsNullOrEmpty(email));
            return default(SocialPipelineUser);
        }

        public SocialPipelineUser FindByUserId(int userId)
        {
            Contract.Requires(userId > 0);
            return default(SocialPipelineUser);
        }

        public List<SocialPipelineUser> FindAllUsersInCompanyByUserId(int userId)
        {
            Contract.Requires(userId > 0);
            return default(List<SocialPipelineUser>);
        }

        public void AddUser(SocialPipelineUser user)
        {
            Contract.Requires(!string.IsNullOrEmpty(user.FirstName));
            Contract.Requires(!string.IsNullOrEmpty(user.LastName));
            Contract.Requires(!string.IsNullOrEmpty(user.Email));
            Contract.Requires(!string.IsNullOrEmpty(user.Password));
        }
    }
}
