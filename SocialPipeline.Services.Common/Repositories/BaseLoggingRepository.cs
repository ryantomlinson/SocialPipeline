using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace SocialPipeline.Services.Common.Repositories
{
    public class BaseLoggingRepository
    {
        public string ConnectionString;

        public BaseLoggingRepository()
        {
            this.ConnectionString = ConfigurationManager.ConnectionStrings["SocialPipelineLoggingDb"].ToString();
        }
    }
}
