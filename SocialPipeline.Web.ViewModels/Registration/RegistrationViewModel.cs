using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialPipeline.Web.ViewModels.Registration
{
    public class RegistrationViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string Password { get; set; }
    }
}
