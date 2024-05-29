using Domain.Interfaces.Cadastros.AplicacaoRecomendacoesTecnicas;
using Domain.Interfaces.Cadastros.AplicacaoRelatorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Validators;

namespace WebApp.Controllers.AplicacaoRelatorio
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AplicacaoRelatorioController : ControllerBase
    {
        private IAplicacaoRelatorioService _baseAplicacaoRelatorioService;

        public AplicacaoRelatorioController(IAplicacaoRelatorioService baseAplicacaoRelatorioService)
        {
            _baseAplicacaoRelatorioService = baseAplicacaoRelatorioService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Entities.Entidades.Cadastros.Aplicacao.AplicacaoRelatorio aplicacaoRelatorio)
        {
            if (aplicacaoRelatorio == null)
                return NotFound();

            return Execute(() => _baseAplicacaoRelatorioService.Inserir<AplicacaoRelatorioValidator>(aplicacaoRelatorio).Id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Entities.Entidades.Cadastros.Aplicacao.AplicacaoRelatorio aplicacaoRelatorio)
        {
            if (aplicacaoRelatorio == null)
                return NotFound();

            return Execute(() => _baseAplicacaoRelatorioService.Atualizar<AplicacaoRelatorioValidator>(aplicacaoRelatorio));
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _baseAplicacaoRelatorioService.Remover(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Execute(() => _baseAplicacaoRelatorioService.ListarTodasAplicacoesRelatorio());

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

            return Execute(() => _baseAplicacaoRelatorioService.BuscarPorId(id));
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
