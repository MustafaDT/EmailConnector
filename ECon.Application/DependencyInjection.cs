using System.Data;
using ECon.Application.Interfaces.IRepositories;
using ECon.Application.Interfaces.IServices;
using ECon.Application.Services;
using ECon.Application.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.SqlClient;
using Serilog;

namespace ECon.Application
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, string? connectionString)
        {
            services.AddTransient<IDbConnection>(_ => new SqlConnection(connectionString));

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSerilog();
            });

           

            services.AddTransient<IEmailRequestService, EmailRequestService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IEmailRequestRepository, EmailRequestRepository>();
            services.AddTransient<IEmailSendAttemptRepository, EmailSendAttemptRepository>();

        }
    }
}