using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TheRealBank.Contexts
{
    public class MySqlContext : MainContext
    {
        private readonly IConfiguration configuration;

        public MySqlContext(DbContextOptions<MySqlContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var cs = configuration.GetConnectionString("DefaultConnection") ?? configuration["ConnectionString"];
                var versionText = configuration["Database:MySqlVersion"] ?? "8.0.36-mysql";
                optionsBuilder.UseMySql(cs, ServerVersion.Parse(versionText));
            }
        }
    }
}

