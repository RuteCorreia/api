using Domain.Interfaces.Cadastros.AplicacaoAreaTratada;
using Domain.Interfaces.Cadastros.AplicacaoCaracteristicas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Validators;

namespace WebApp.Controllers.AplicacaoCaracteristicas
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AplicacaoCaracteristicasController : ControllerBase
    {
        private IAplicacaoCaracteristicasService _baseAplicacaoCaracteristicasService;

        public AplicacaoCaracteristicasController(IAplicacaoCaracteristicasService baseAplicacaoCaracteristicasService)
        {
            _baseAplicacaoCaracteristicasService = baseAplicacaoCaracteristicasService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Entities.Entidades.Cadastros.Aplicacao.AplicacaoCaracteristicas aplicacaoCaracteristicas)
        {
            if (aplicacaoCaracteristicas == null)
                return NotFound();

            return Execute(() => _baseAplicacaoCaracteristicasService.Inserir<AplicacaoCaracteristicasValidator>(aplicacaoCaracteristicas).Id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Entities.Entidades.Cadastros.Aplicacao.AplicacaoCaracteristicas aplicacaoCaracteristicas)
        {
            if (aplicacaoCaracteristicas == null)
                return NotFound();

            return Execute(() => _baseAplicacaoCaracteristicasService.Atualizar<AplicacaoCaracteristicasValidator>(aplicacaoCaracteristicas));
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _baseAplicacaoCaracteristicasService.Remover(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Execute(() => _baseAplicacaoCaracteristicasService.ListarTodasAplicacoesCaracteristicas());

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

            return Execute(() => _baseAplicacaoCaracteristicasService.BuscarPorId(id));
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
