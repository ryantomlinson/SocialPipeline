using System.Collections.Generic;

namespace SocialPipeline.Web.ViewModels.Organisations
{
    public class OrganisationsViewModel
    {
        public OrganisationsViewModel()
        {
            Organisations = new List<SocialPipeline.Services.Models.Entities.Organisation.Organisation>();
        }

        public List<SocialPipeline.Services.Models.Entities.Organisation.Organisation> Organisations { get; set; }
    }
}
