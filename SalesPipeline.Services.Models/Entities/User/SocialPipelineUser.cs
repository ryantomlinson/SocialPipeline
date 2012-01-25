﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocialPipeline.Services.Common.Helpers;

namespace SocialPipeline.Services.Models.Entities.User
{
    public class SocialPipelineUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime RegisteredDate { get; set; }
        public bool IsAdmin { get; set; }
        public int CompanyId { get; set; }

        public bool IsValidUser(string password)
        {
            return PasswordService.Md5Hash(password) == this.Password;
        }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }
    }
}
