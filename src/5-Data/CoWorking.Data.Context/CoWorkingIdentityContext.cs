using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace CoWorking.Data.Context
{
    public class CoWorkingIdentityContext : IdentityDbContext<IdentityUser>
    {
        IConfigurationRoot _configuration;
        public CoWorkingIdentityContext()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionStringBuilder.GetConnectionString(_configuration));
            //optionsBuilder.UseSqlServer(@"Server=.;Database=CoWorkingDb;Trusted_Connection=True;");
        }
    }
}