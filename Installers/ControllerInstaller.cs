using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Verification.Data.IRepos;
using Verification.Data.Repos;
using Verification.Filters;

namespace Verification.Installers
{
    public class ControllerInstaller: IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers().AddNewtonsoftJson(s =>
            {
                s.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                s.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<CustomExceptionFilter>();
        }
    }
}