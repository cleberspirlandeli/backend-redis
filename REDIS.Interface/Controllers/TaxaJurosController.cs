using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using REDIS.Interface.Notificacoes;
using ServiceStack.Redis;
using System;

namespace REDIS.Interface.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class TaxaJurosController : BaseController
    {

        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public TaxaJurosController(
            ILogger<TaxaJurosController> logger,
            IConfiguration configuration,
            INotificador notificador
            ) : base(notificador)
        {
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// Retorna a taxa de juros atual com alguns detalhes
        /// </summary>
        /// <returns>Retorna uma classe TaxaJuros</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetTaxaJuros()
        {

            var taxaJuros = new TaxaJuros();
            using (var redisClient = new RedisClient(_configuration.GetValue<string>("Host_Redis")))
            {
                var key = ChaveTaxaJuros.GetKeyJurosComposto();
                taxaJuros = redisClient.Get<TaxaJuros>(key);
            }

            var tipoTaxaJuros = Enum.GetName(typeof(TipoTaxaJurosEnum), taxaJuros.Tipo);

            var message = $"Request: TaxaJurosController - Method: GetTaxaJuros. Info: Taxa: {taxaJuros.Taxa} - Tipo: { tipoTaxaJuros } - Hora: {DateTime.Now}";
            _logger.LogInformation(message);

            return CustomResponse(taxaJuros);
        }
    }
}
