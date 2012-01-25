using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocialPipeline.Services.Models.Entities.User;

namespace SocialPipeline.Web.ViewModels.Users
{
    public class UsersViewModel
    {
        public UsersViewModel()
        {
            Users = new List<SocialPipelineUser>();
        }

        public List<SocialPipelineUser> Users { get; set; }
    }
}
