using System.Data;
using JadwaEmailConnector.Application.Interfaces.IServices;
using JadwaEmailConnector.Application.Services;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace JadwaEmailConnector.Application
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, string? connectionString)
        {
            services.AddTransient<IDbConnection>(provider => new SqlConnection(connectionString));

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSerilog();
            });

           

            services.AddTransient<IEmailRequestService, EmailRequestRepository>();
          
        }
    }
}