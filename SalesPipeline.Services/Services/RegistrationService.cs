using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using SocialPipeline.Data.Common.Entities;
using SocialPipeline.Data.Common.Exceptions;
using SocialPipeline.Data.Dal.Interfaces;
using SocialPipeline.Services.Common.Exceptions;
using SocialPipeline.Services.Common.Helpers;
using SocialPipeline.Services.Common.Interfaces;
using SocialPipeline.Services.Interfaces;
using SocialPipeline.Services.Models.Entities.Company;
using SocialPipeline.Services.Models.Entities.User;
using RegistrationException = SocialPipeline.Data.Common.Exceptions.RegistrationException;

namespace SocialPipeline.Services.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IRegistrationRepository registrationRepository;
        private readonly IUserService userService;
        private readonly ICompanyService companyService;
        private readonly IDatabaseLogger logger;

        public RegistrationService(IRegistrationRepository registrationRepository,
                                    IUserService userService,
                                    ICompanyService companyService,
                                    IDatabaseLogger logger)
        {
            this.registrationRepository = registrationRepository;
            this.userService = userService;
            this.companyService = companyService;
            this.logger = logger;
        }

        /// <exception cref="UnableToRegisterUserException">Thrown when there are issues registering a new user.</exception>
        public int RegisterUser(SocialPipelineUser user, Company company)
        {
            try
            {
                var userDto = Mapper.Map<SocialPipelineUser, SocialPipelineUserDto>(user);
                var companyDto = Mapper.Map<Company, CompanyDto>(company);

                userDto.Password = PasswordService.Md5Hash(userDto.Password);

                EnsureUserIsNotAlreadyRegistered(user);

                EnsureCompanyIsNotAlreadyRegistered(company);

                return registrationRepository.RegisterUser(userDto, companyDto);
            }
            catch (RegistrationException exception)
            {
                logger.LogException("RegisterUser: Unable to register new user", exception);
                throw new UnableToRegisterUserException("RegisterUser: Unable to register new user", exception);
            }
            catch (SocialPipelineDatabaseConnectionException exception)
            {
                logger.LogException("RegisterUser: Unable to register new user due to database connection issue", exception);
                throw new UnableToRegisterUserException("RegisterUser: Unable to register new user due to database connection issue", exception);
            }
            catch (UserAlreadyRegisteredException exception)
            {
                logger.LogException(string.Format("RegisterUser: User already exists and registered with this email {0}", user.Email));
                throw;
            }
            catch (CompanyAlreadyExistsException exception)
            {
                logger.LogException(string.Format("RegisterUser: Company already exists and registered with this name {0}", company.Name));
                throw;
            }
            catch (Exception exception)
            {
                logger.LogException("RegisterUser: Unhandled Exception", exception);
                throw new UnableToRegisterUserException("RegisterUser: Unhandled Exception", exception);
            }
        }

        private void EnsureCompanyIsNotAlreadyRegistered(Company company)
        {
            try
            {
                var registeredCompany = companyService.FindByName(company.Name);
                if (registeredCompany != null)
                    throw new CompanyAlreadyExistsException("RegisterUser: The company already exists");
            }
            catch (FindCompanyException)
            {
                //If the company doesn't exist that's good.
            }
        }

        private void EnsureUserIsNotAlreadyRegistered(SocialPipelineUser user)
        {
            try
            {
                var registeredUser = userService.FindByEmailAddress(user.Email);
                if (registeredUser != null)
                    throw new UserAlreadyRegisteredException("RegisterUser: This user already exists");
            }
            catch (FindUserException)
            {
                //If the user doesn't exist that's good. We can continue!
            }
        }
    }
}
