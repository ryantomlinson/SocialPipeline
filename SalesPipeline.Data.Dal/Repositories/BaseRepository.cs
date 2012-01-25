using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace SocialPipeline.Data.Dal.Repositories
{
    public class BaseRepository
    {
        public string ConnectionString;

        public BaseRepository()
        {
            this.ConnectionString = ConfigurationManager.ConnectionStrings["SocialPipelineDb"].ToString();
        }
    }
}
