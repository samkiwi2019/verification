using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Verification.Api.Installers
{
    public interface IInstaller
    {
        void InstallService(IServiceCollection services,IConfiguration configuration);
    }
}