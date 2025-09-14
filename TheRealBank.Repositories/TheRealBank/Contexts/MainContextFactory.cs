using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TheRealBank.Contexts
{
    public class MainContextFactory : IDesignTimeDbContextFactory<MainContext>
    {
        public MainContext CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory(); // StartupProject (UI)
            var config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            var provider = config["Database:Provider"] ?? "SqlServer";
            var connectionString = config.GetConnectionString("DefaultConnection")
                                   ?? "Server=localhost;Database=FirstDataBase;User Id=sa;Password=1senha23;Encrypt=False;TrustServerCertificate=True";

            var options = new DbContextOptionsBuilder<MainContext>();

            switch (provider)
            {
                case "SqlServer":
                    options.UseSqlServer(connectionString);
                    break;
                case "MySql":
                    var versionText = config["Database:MySqlVersion"] ?? "8.0.36-mysql";
                    options.UseMySql(connectionString, ServerVersion.Parse(versionText));
                    break;
                default:
                    throw new InvalidOperationException($"Provedor '{provider}' não suportado.");
            }

            return new MainContext(options.Options);
        }
    }
}
