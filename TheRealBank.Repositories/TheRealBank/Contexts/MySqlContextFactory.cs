using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TheRealBank.Contexts
{
    public class MySqlContextFactory : IDesignTimeDbContextFactory<MySqlContext>
    {
        public MySqlContext CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory();
            var config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            var cs = config.GetConnectionString("DefaultConnection")
                     ?? "server=localhost;port=3306;database=TheRealBank;user=root;password=SUASENHA;SslMode=None";
            var versionText = config["Database:MySqlVersion"] ?? "8.0.36-mysql";

            var options = new DbContextOptionsBuilder<MySqlContext>()
                .UseMySql(cs, ServerVersion.Parse(versionText))
                .Options;

            return new MySqlContext(options, config);
        }
    }
}