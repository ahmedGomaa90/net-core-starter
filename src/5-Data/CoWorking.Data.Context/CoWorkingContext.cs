using Microsoft.EntityFrameworkCore;
using CoWorking.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace CoWorking.Data.Context
{
    public class CoWorkingContext : DbContext
    {
        IConfigurationRoot _configuration;
        public CoWorkingContext()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath("e:\\CoWorking\\CoWorking")
                .AddJsonFile("2-Services/CoWorking.Services.WebApi/appsettings.json", optional: false, reloadOnChange: true);
            _configuration = builder.Build();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionStringBuilder.GetConnectionString(_configuration));

            //optionsBuilder.UseSqlServer(@"Server=.;Database=CoWorkingDb;Trusted_Connection=True;");
        }

        public virtual DbSet<Dummy> Dummies { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }


}
