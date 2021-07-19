using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Verification.Data;

namespace Verification.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            // to connect mysql 
            services.AddDbContext<MyContext>(opt =>
                opt.UseMySql(configuration.GetConnectionString("connection"),
                    new MySqlServerVersion(new Version(8, 0, 23))));
        }
    }
}