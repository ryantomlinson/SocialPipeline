using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialPipeline.Services.Common.Interfaces
{
    public interface ISessionIdentity
    {
		string	IpAddress				{ get; set; }
		int		UserId					{ get; set; }
    }
}
