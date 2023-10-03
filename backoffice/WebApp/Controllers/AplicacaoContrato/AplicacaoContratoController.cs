using Domain.Interfaces.Cadastros.AplicacaoCaracteristicas;
using Domain.Interfaces.Cadastros.AplicacaoContrato;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Validators;

namespace WebApp.Controllers.AplicacaoContrato
{
    [Route("api/[controller]")]
    [ApiController]
    public class AplicacaoContratoController : ControllerBase
    {
        private IAplicacaoContratoService _baseAplicacaoContratoService;

        public AplicacaoContratoController(IAplicacaoContratoService baseAplicacaoContratoService)
        {
            _baseAplicacaoContratoService = baseAplicacaoContratoService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Entities.Entidades.Cadastros.Aplicacao.AplicacaoContrato aplicacaoContrato)
        {
            if (aplicacaoContrato == null)
                return NotFound();

            return Execute(() => _baseAplicacaoContratoService.Inserir<AplicacaoContratoValidator>(aplicacaoContrato).Id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Entities.Entidades.Cadastros.Aplicacao.AplicacaoContrato aplicacaoContrato)
        {
            if (aplicacaoContrato == null)
                return NotFound();

            return Execute(() => _baseAplicacaoContratoService.Atualizar<AplicacaoContratoValidator>(aplicacaoContrato));
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _baseAplicacaoContratoService.Remover(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Execute(() => _baseAplicacaoContratoService.Listar());

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

            return Execute(() => _baseAplicacaoContratoService.BuscarPorId(id));
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
