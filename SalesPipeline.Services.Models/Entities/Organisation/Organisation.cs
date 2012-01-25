using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocialPipeline.Services.Models.Entities.User;

namespace SocialPipeline.Services.Models.Entities.Organisation
{
    public class Organisation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public string Twitter { get; set; }
        public string Facebook { get; set; }
        public DateTime DateAdded { get; set; }
        public bool Active { get; set; }
        public SocialPipelineUser CreatedBy { get; set; }
    }
}
