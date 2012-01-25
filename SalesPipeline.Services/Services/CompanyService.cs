using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using SocialPipeline.Data.Common.Entities;
using SocialPipeline.Data.Common.Exceptions;
using SocialPipeline.Data.Dal.Interfaces;
using SocialPipeline.Services.Common.Exceptions;
using SocialPipeline.Services.Common.Interfaces;
using SocialPipeline.Services.Interfaces;
using SocialPipeline.Services.Models.Entities.Company;

namespace SocialPipeline.Services.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository companyRepository;
        private readonly IDatabaseLogger logger;

        public CompanyService(ICompanyRepository companyRepository,
                                IDatabaseLogger logger)
        {
            this.companyRepository = companyRepository;
            this.logger = logger;
        }

        /// <exception cref="FindCompanyException">Thrown when there are issues finding the company or the company does not exist.</exception>
        public Company FindByName(string name)
        {
            try
            {
                var companyDto = companyRepository.FindByName(name);
                var company = Mapper.Map<CompanyDto, Company>(companyDto);

                return company;
            }
            catch (UnknownCompanyException exception)
            {
                logger.LogException("FindByName: Unable to find company", exception);
                throw new FindCompanyException("FindByName: Company does not exist.");
            }
            catch (SocialPipelineDatabaseConnectionException exception)
            {
                logger.LogException("FindByName: Unable to find company due to database connection issue.", exception);
                throw new FindCompanyException("FindByName: Problems connecting to the database.");
            }
            catch (Exception exception)
            {
                logger.LogException("FindByName: Unhandled Exception", exception);
                throw new FindCompanyException("FindByName: Unknown issue.");
            }
        }
    }
}
