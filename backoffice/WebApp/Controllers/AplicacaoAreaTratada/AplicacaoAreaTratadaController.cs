using Domain.Interfaces.Cadastros.Aplicacao;
using Domain.Interfaces.Cadastros.AplicacaoAreaTratada;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Validators;

namespace WebApp.Controllers.AplicacaoAreaTratada
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AplicacaoAreaTratadaController : ControllerBase
    {
        private IAplicacaoAreaTratadaService _baseAplicacaoAreaTratadaService;

        public AplicacaoAreaTratadaController(IAplicacaoAreaTratadaService baseAplicacaoAreaTratadaService)
        {
            _baseAplicacaoAreaTratadaService = baseAplicacaoAreaTratadaService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Entities.Entidades.Cadastros.Aplicacao.AplicacaoAreaTratada aplicacaoAreaTratada)
        {
            if (aplicacaoAreaTratada == null)
                return NotFound();

            return Execute(() => _baseAplicacaoAreaTratadaService.Inserir<AplicacaoAreaTratadaValidator>(aplicacaoAreaTratada).Id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Entities.Entidades.Cadastros.Aplicacao.AplicacaoAreaTratada aplicacaoAreaTratada)
        {
            if (aplicacaoAreaTratada == null)
                return NotFound();

            return Execute(() => _baseAplicacaoAreaTratadaService.Atualizar<AplicacaoAreaTratadaValidator>(aplicacaoAreaTratada));
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _baseAplicacaoAreaTratadaService.Remover(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Execute(() => _baseAplicacaoAreaTratadaService.ListarTodasAplicacoesAreaTratadas());

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

            return Execute(() => _baseAplicacaoAreaTratadaService.BuscarPorId(id));
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
