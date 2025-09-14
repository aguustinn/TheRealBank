using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheRealBank.Contexts;
using TheRealBank.Repositories.Users;

namespace TheRealBank.Repositories
{
    public static class ExtensionMethods
    {
        public static IServiceCollection AddDesignerRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            var provider = configuration["Database:Provider"];
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrWhiteSpace(provider))
                throw new InvalidOperationException("Database:Provider é obrigatório em appsettings.json.");
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("ConnectionStrings:DefaultConnection é obrigatório em appsettings.json.");

            services.AddDbContext<MainContext>(options =>
            {
                switch (provider)
                {
                    case "SqlServer":
                        options.UseSqlServer(connectionString, sql =>
                        {
                            sql.EnableRetryOnFailure(
                                maxRetryCount: 5,
                                maxRetryDelay: TimeSpan.FromSeconds(10),
                                errorNumbersToAdd: null);
                        });
                        break;

                    case "MySql":
                        var versionText = configuration["Database:MySqlVersion"] ?? "8.0.36-mysql";
                        options.UseMySql(connectionString, ServerVersion.Parse(versionText), my =>
                        {
                            my.EnableRetryOnFailure(
                                maxRetryCount: 5,
                                maxRetryDelay: TimeSpan.FromSeconds(10),
                                errorNumbersToAdd: null);
                        });
                        break;

                    default:
                        throw new InvalidOperationException($"Provedor '{provider}' não suportado.");
                }
            });

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            return services;
        }
    }
}

