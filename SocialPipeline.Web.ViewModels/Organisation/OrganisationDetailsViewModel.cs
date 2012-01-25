using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialPipeline.Web.ViewModels.Organisations
{
    public class OrganisationDetailsViewModel
    {
        public SocialPipeline.Services.Models.Entities.Organisation.Organisation Organisation { get; set; }
        public bool HasUpdates { get; set; }
        public bool HasPeople { get; set; }
        public bool HasDeals { get; set; }
        public bool HasActivities { get; set; }
    }
}
