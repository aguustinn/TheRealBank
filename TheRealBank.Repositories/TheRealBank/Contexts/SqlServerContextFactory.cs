using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TheRealBank.Contexts
{
    public class SqlServerContextFactory : IDesignTimeDbContextFactory<SqlServerContext>
    {
        public SqlServerContext CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory();
            var config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            var cs = config.GetConnectionString("DefaultConnection")
                     ?? "Server=localhost;Database=FirstDataBase;User Id=sa;Password=1senha23;Encrypt=False;TrustServerCertificate=True";

            var options = new DbContextOptionsBuilder<SqlServerContext>()
                .UseSqlServer(cs)
                .Options;

            // Correção: passe 'options' diretamente (sem new DbContextOptions<SqlServerContext>(options))
            return new SqlServerContext(options, config);
        }
    }
}