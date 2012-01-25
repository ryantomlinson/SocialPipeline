using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AutoMapperAssist;
using SocialPipeline.Data.Common.Entities;
using SocialPipeline.Services.Models.Entities.Company;
using SocialPipeline.Services.Models.Entities.Organisation;
using SocialPipeline.Services.Models.Entities.User;

namespace SocialPipeline.Services.Configuration.Mapping
{
    public static class MappingBootstrapper
    {
        public static void Register()
        {
            Mapper.CreateMap<OrganisationDto, Organisation>()
                .ForMember(dest => dest.CreatedBy, src => src.MapFrom(x => x.CreatedBy));
            Mapper.CreateMap<Organisation, OrganisationDto>()
                .ForMember(dest => dest.CreatedBy, src => src.MapFrom(x => x.CreatedBy));
            Mapper.CreateMap<SocialPipelineUserDto, SocialPipelineUser>();
            Mapper.CreateMap<SocialPipelineUser, SocialPipelineUserDto>();
            Mapper.CreateMap<CompanyDto, Company>();
            Mapper.CreateMap<Company, CompanyDto>();
        }
    }
}
