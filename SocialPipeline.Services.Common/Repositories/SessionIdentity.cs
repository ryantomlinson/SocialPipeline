using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocialPipeline.Services.Common.Interfaces;

namespace SocialPipeline.Services.Common.Repositories
{
    public class SessionIdentity : ISessionIdentity
    {
		public string	IpAddress				{ get; set; }
		public int		UserId					{ get; set; }
    }
}
