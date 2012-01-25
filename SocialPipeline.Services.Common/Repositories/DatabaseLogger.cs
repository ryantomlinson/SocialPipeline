using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using SocialPipeline.Services.Common.Interfaces;

namespace SocialPipeline.Services.Common.Repositories
{
    public class DatabaseLogger : BaseLoggingRepository, IDatabaseLogger
    {
        private readonly ISessionIdentity sessionIdentity;

        public DatabaseLogger(ISessionIdentity sessionIdentity)
        {
            this.sessionIdentity = sessionIdentity;
        }

        public void LogException(string message, Exception exception = null)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    
                    string exceptionString = exception == null ? "" : exception.ToString();
                    string sqlInsertExceptionQuery = @"INSERT INTO [SalesPipeline.Logging].[dbo].[Exception]
                                                           ([Message]
                                                           ,[Exception]
                                                           ,[UserId])
                                                     VALUES
                                                           (@Message
                                                           ,@Exception
                                                           ,@UserId)
                                                ";

                    connection.Execute(sqlInsertExceptionQuery,
                                      new
                                          {
                                              Message = message,
                                              Exception = exceptionString,
                                              UserId = sessionIdentity.UserId
                                          });
 
                
                    connection.Close();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
