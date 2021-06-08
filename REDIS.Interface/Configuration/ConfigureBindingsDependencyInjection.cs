using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using REDIS.Interface.Notificacoes;

namespace REDIS.Interface.Configuration
{
    public class ConfigureBindingsDependencyInjection
    {
        public static void RegisterBindings(IServiceCollection services, IConfiguration configuration)
        {
            #region Others
            services.AddScoped<INotificador, Notificador>();
            #endregion

            #region ApplicationService
            #endregion

            #region Repository
            #endregion

            #region UnitOfWork
            #endregion

            #region ServiceBus
            #endregion

        }
    }
}
