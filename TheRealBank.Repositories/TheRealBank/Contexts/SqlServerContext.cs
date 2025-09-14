using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TheRealBank.Contexts
{
    public class SqlServerContext : MainContext
    {
        private readonly IConfiguration configuration;

        public SqlServerContext(DbContextOptions<SqlServerContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection")
                                       ?? configuration["ConnectionString"];
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
