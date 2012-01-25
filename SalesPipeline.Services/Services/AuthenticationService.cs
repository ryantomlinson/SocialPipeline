using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocialPipeline.Services.Common.Exceptions;
using SocialPipeline.Services.Common.Interfaces;
using SocialPipeline.Services.Interfaces;
using SocialPipeline.Services.Models.Entities.User;


namespace SocialPipeline.Services.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService userService;
        private readonly IDatabaseLogger logger;

        public AuthenticationService(IUserService userService, IDatabaseLogger logger)
        {
            this.userService = userService;   
            this.logger = logger;
        }

        /// <exception cref="AuthenticationException">Thrown when the user does not exist.</exception>
        /// <exception cref="LogonException">Thrown when there are issues logging in not related to finding the user.</exception>
        public SocialPipelineUser LogonUser(string email, string password)
        {
            try
            {
                var user = userService.FindByEmailAddress(email);
                if (user == null)
                    throw new ArgumentException();

                if (!user.IsValidUser(password))
                    throw new AuthenticationException("LogonUser: Invalid password");

                return user;
            }
            catch (FindUserException exception)
            {
                logger.LogException("LogonUser: Unable to find user", exception);
                throw new AuthenticationException("LogonUser: Unable to find the user");
            }
            catch (AuthenticationException exception)
            {
                logger.LogException("LogonUser: Authentication exception", exception);
                throw;
            }
            catch (Exception exception)
            {
                logger.LogException("LogonUser: Unhandled exception", exception);
                throw new LogonException("LogonUser: Unhandled Exception");
            }
        }
    }
}
