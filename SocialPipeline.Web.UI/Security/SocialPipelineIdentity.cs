using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace SocialPipeline.Web.UI.Security
{
    public class SocialPipelineIdentity : IIdentity
    {
        public SocialPipelineIdentity(string name)
        {
            Name = name;
        }

        public string Name
        {
            get; private set;
        }

        public string AuthenticationType
        {
            get { return "Custom"; }
        }

        public bool IsAuthenticated
        {
            get { return !string.IsNullOrEmpty(Name); }
        }
    }
}