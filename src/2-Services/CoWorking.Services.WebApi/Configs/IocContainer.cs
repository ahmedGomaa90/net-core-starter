using CoWorking.Business.Managers;
using CoWorking.Data.Repositories;
using CoWorking.Data.Repositories.Repositories;
using CoWorking.Domain.BusinessConracts.Managers;
using CoWorking.Domain.DataContracts.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CoWorking.Services.WebApi.Configs
{
    public class IocContainer
    {
        public static void Register(IServiceCollection services)
        {
            // register managers
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IDummyManager, DummyManager>();

            // register Repos
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDummyRepository, DummyRepository>();

            // others
            services.AddScoped<IQuerableUnitOfWork, QueryableUnitOfWork>();
        }
    }
}