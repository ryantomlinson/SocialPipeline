using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialPipeline.Data.Common.Entities
{
    public class OrganisationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public string Twitter { get; set; }
        public string Facebook { get; set; }
        public DateTime DateAdded { get; set; }
        public bool Active { get; set; }
        public SocialPipelineUserDto CreatedBy { get; set; }
    }
}
