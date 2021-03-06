using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack.Redis;
using System;

namespace REDIS.Interface.Configuration
{
    public class RedisConfig
    {
        private static readonly string _host = "localhost:6379";

        public static void StartRedis(IServiceCollection services, IConfiguration configuration)
        {
            var taxaJuros = new TaxaJuros
            {
                Taxa = 0.01,
                Descricao = "Taxa para Juros Composto valor fixo.",
                DataCadastro = DateTime.Now,
                Tipo = (int)TipoTaxaJurosEnum.Composto
            };


            using (var redisClient = new RedisClient(configuration.GetValue<string>("Host_Redis")))
            {
                var key = ChaveTaxaJuros.GetKeyJurosComposto();
                redisClient.Set<TaxaJuros>(key, taxaJuros);
            }
        }
    }
}
