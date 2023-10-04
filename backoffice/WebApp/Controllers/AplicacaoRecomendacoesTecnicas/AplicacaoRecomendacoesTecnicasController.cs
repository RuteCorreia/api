using Domain.Interfaces.Cadastros.AplicacaoCroquiImportacao;
using Domain.Interfaces.Cadastros.AplicacaoRecomendacoesTecnicas;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Validators;

namespace WebApp.Controllers.AplicacaoRecomendacoesTecnicas
{
    [Route("api/[controller]")]
    [ApiController]
    public class AplicacaoRecomendacoesTecnicasController : ControllerBase
    {
        private IAplicacaoRecomendacoesTecnicasService _baseAplicacaoRecomendacoesTecnicasService;

        public AplicacaoRecomendacoesTecnicasController(IAplicacaoRecomendacoesTecnicasService baseAplicacaoRecomendacoesTecnicasService)
        {
            _baseAplicacaoRecomendacoesTecnicasService = baseAplicacaoRecomendacoesTecnicasService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Entities.Entidades.Cadastros.Aplicacao.AplicacaoRecomendacoesTecnicas aplicacaoRecomendacoesTecnicas)
        {
            if (aplicacaoRecomendacoesTecnicas == null)
                return NotFound();

            return Execute(() => _baseAplicacaoRecomendacoesTecnicasService.Inserir<AplicacaoRecomendacoesTecnicasValidator>(aplicacaoRecomendacoesTecnicas).Id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Entities.Entidades.Cadastros.Aplicacao.AplicacaoRecomendacoesTecnicas aplicacaoRecomendacoesTecnicas)
        {
            if (aplicacaoRecomendacoesTecnicas == null)
                return NotFound();

            return Execute(() => _baseAplicacaoRecomendacoesTecnicasService.Atualizar<AplicacaoRecomendacoesTecnicasValidator>(aplicacaoRecomendacoesTecnicas));
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _baseAplicacaoRecomendacoesTecnicasService.Remover(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Execute(() => _baseAplicacaoRecomendacoesTecnicasService.ListarTodasAplicacoesRecomendacoesTecnicas());

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

            return Execute(() => _baseAplicacaoRecomendacoesTecnicasService.BuscarPorId(id));
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
