using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialPipeline.Web.ViewModels.Logon
{
    public class LogonViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
