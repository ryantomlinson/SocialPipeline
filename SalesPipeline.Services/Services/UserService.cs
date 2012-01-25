using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocialPipeline.Data.Common.Entities;
using SocialPipeline.Data.Common.Exceptions;
using SocialPipeline.Data.Dal.Interfaces;
using SocialPipeline.Services.Common.Exceptions;
using SocialPipeline.Services.Common.Helpers;
using SocialPipeline.Services.Common.Interfaces;
using SocialPipeline.Services.Interfaces;
using SocialPipeline.Services.Models.Entities.User;
using AutoMapper;

namespace SocialPipeline.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IDatabaseLogger logger;

        public UserService(IUserRepository userRepository, IDatabaseLogger logger)
        {
            this.userRepository = userRepository;
            this.logger = logger;
        }

        /// <exception cref="FindUserException">Thrown when there are issues finding the user of the user does not exist.</exception>
        public SocialPipelineUser FindByEmailAddress(string email)
        {
            try
            {
                var userDto = userRepository.FindByEmailAddress(email);
                var user = Mapper.Map<SocialPipelineUserDto, SocialPipelineUser>(userDto);
                return user;
            }
            catch (UnknownUserException exception)
            {
                logger.LogException("FindByEmailAddress: Unable to find user", exception);
                throw new FindUserException("FindByEmailAddress: User does not exist.");
            }
            catch (SocialPipelineDatabaseConnectionException exception)
            {
                logger.LogException("FindByEmailAddress: Unable to find user due to database connection issue.", exception);
                throw new FindUserException("FindByEmailAddress: Problems connecting to the database.");
            }
            catch (Exception exception)
            {
                logger.LogException("FindByEmailAddress: Unhandled Exception", exception);
                throw new FindUserException("FindByEmailAddress: Unknown issue.");
            }
            
        }

        /// <exception cref="FindUserException">Thrown when there are issues finding the user of the user does not exist.</exception>
        public SocialPipelineUser FindByUserId(int userId)
        {
            try
            {
                var userDto = userRepository.FindByUserId(userId);
                var user = Mapper.Map<SocialPipelineUserDto, SocialPipelineUser>(userDto);
                return user;
            }
            catch (UnknownUserException exception)
            {
                logger.LogException("FindByUserId: Unable to find user", exception);
                throw new FindUserException("FindByUserId: User does not exist.");
            }
            catch (SocialPipelineDatabaseConnectionException exception)
            {
                logger.LogException("FindByUserId: Unable to find user due to database connection issue.", exception);
                throw new FindUserException("FindByUserId: Problems connecting to the database.");
            }
            catch (Exception exception)
            {
                logger.LogException("FindByUserId: Unhandled Exception", exception);
                throw new FindUserException("FindByUserId: Unknown issue.");
            }
        }

        /// <exception cref="FindUserException">Thrown when there are issues finding the user of the user does not exist.</exception>
        public List<SocialPipelineUser> FindAllUsersInCompanyByUserId(int userId)
        {
            try
            {
                var userDtos = userRepository.FindAllUsersInCompanyByUserId(userId);
                var users = new List<SocialPipelineUser>(userDtos.Count);
                users.AddRange(userDtos.Select(Mapper.Map<SocialPipelineUserDto, SocialPipelineUser>));

                return users;
            }
            catch (UnknownUserException exception)
            {
                logger.LogException("FindAllUsersInCompanyByUserId: Unable to find users", exception);
                throw new FindUserException("FindAllUsersInCompanyByUserId: Users does not exist.");
            }
            catch (SocialPipelineDatabaseConnectionException exception)
            {
                logger.LogException("FindAllUsersInCompanyByUserId: Unable to find users due to database connection issue.", exception);
                throw new FindUserException("FindAllUsersInCompanyByUserId: Problems connecting to the database.");
            }
            catch (Exception exception)
            {
                logger.LogException("FindAllUsersInCompanyByUserId: Unhandled Exception", exception);
                throw new FindUserException("FindAllUsersInCompanyByUserId: Unknown issue.");
            }
        }

        /// <exception cref="UnableToAddUserException">Thrown when there is an issue adding a new user</exception>
        public void AddUser(SocialPipelineUser user)
        {
            try
            {
                //Create user
                var userDto = Mapper.Map<SocialPipelineUser, SocialPipelineUserDto>(user);

                userDto.Password = PasswordService.Md5Hash(userDto.Password);

                userRepository.AddUser(userDto);
                //Email user
                //TODO: Add ability to email using IEmailService
            }
            catch (UnableToAddUserException exception)
            {
                logger.LogException("AddUser: Unable to find users", exception);
                throw new UnableToAddUserException("AddUser: Users does not exist.");
            }
            catch (SocialPipelineDatabaseConnectionException exception)
            {
                logger.LogException("AddUser: Unable to find users due to database connection issue.", exception);
                throw new UnableToAddUserException("AddUser: Problems connecting to the database.");
            }
            catch (Exception exception)
            {
                logger.LogException("AddUser: Unhandled Exception", exception);
                throw new UnableToAddUserException("AddUser: Unknown issue.");
            }
        }
    }
}
