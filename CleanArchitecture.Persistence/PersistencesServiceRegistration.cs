using System.Data;

using Boss.Gateway.Persistence.Repositories;
using CleanArchitecture.Application.Contracts.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;


namespace CleanArchitecture.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionString:Postgres"];
            services.AddScoped<IDbConnection>(db =>
            {
                var connection = new NpgsqlConnection(connectionString);
                connection.Open();
                return connection;
            });

           services.AddScoped<ICompanyRepository, CompanyRepository>();
            return services;
        }
    }
}