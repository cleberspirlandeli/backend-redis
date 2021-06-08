using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using REDIS.Interface.Notificacoes;
using System;
using System.Linq;

namespace REDIS.Interface.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly INotificador _notificador;

        protected Guid UsuarioId { get; }
        protected bool UsuarioAutenticado { get; }
        protected ILogger _loggerBase { get; }

        protected BaseController(INotificador notificador
            //ILogger logger
            )
        {
            //_logger = logger;
            _notificador = notificador;

        }

        // Validação de notificações de erro
        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (OperacaoValida())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notificador.ObterNotificacoes().Select(x => x.Mensagem)
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotificarErroModelInvalida(modelState);
            return CustomResponse();
        }

        // Validação  de ModelState
        protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                var mensagemErro = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificarErro(mensagemErro);
            }
        }

        protected void NotificarErro(string mensagemErro)
        {
            _notificador.Handle(new Notificacao(mensagemErro));
        }

        // Validação operação de negocio
    }
}
