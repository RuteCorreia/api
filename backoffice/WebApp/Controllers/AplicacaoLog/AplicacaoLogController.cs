using Domain.Interfaces.Cadastros.AplicacaoCroquiImportacao;
using Domain.Interfaces.Cadastros.AplicacaoLog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Validators;

namespace WebApp.Controllers.AplicacaoLog
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AplicacaoLogController : ControllerBase
    {
        private IAplicacaoLogService _baseAplicacaoLogService;

        public AplicacaoLogController(IAplicacaoLogService baseAplicacaoLogService)
        {
            _baseAplicacaoLogService = baseAplicacaoLogService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Entities.Entidades.Cadastros.Aplicacao.AplicacaoLog aplicacaoLog)
        {
            if (aplicacaoLog == null)
                return NotFound();

            return Execute(() => _baseAplicacaoLogService.Inserir<AplicacaoLogValidator>(aplicacaoLog).Id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Entities.Entidades.Cadastros.Aplicacao.AplicacaoLog aplicacaoLog)
        {
            if (aplicacaoLog == null)
                return NotFound();

            return Execute(() => _baseAplicacaoLogService.Atualizar<AplicacaoLogValidator>(aplicacaoLog));
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _baseAplicacaoLogService.Remover(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Execute(() => _baseAplicacaoLogService.ListarTodasAplicacoesLog());

            }
            catch (Exception ex)
            {
                var json = JsonConvert.SerializeObject(ex)!;
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0)
                return NotFound();

            return Execute(() => _baseAplicacaoLogService.BuscarPorId(id));
        }

        private IActionResult Execute(Func<object> func)
        {
            try
            {
                var result = func();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
