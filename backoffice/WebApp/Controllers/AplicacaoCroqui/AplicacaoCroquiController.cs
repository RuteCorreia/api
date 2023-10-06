using Domain.Interfaces.Cadastros.AplicacaoAreaTratada;
using Domain.Interfaces.Cadastros.AplicacaoCroqui;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Validators;

namespace WebApp.Controllers.AplicacaoCroqui
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AplicacaoCroquiController : ControllerBase
    {
        private IAplicacaoCroquiService _baseAplicacaoCroquiService;

        public AplicacaoCroquiController(IAplicacaoCroquiService baseAplicacaoCroquiService)
        {
            _baseAplicacaoCroquiService = baseAplicacaoCroquiService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Entities.Entidades.Cadastros.Aplicacao.AplicacaoCroqui aplicacaoCroqui)
        {
            if (aplicacaoCroqui == null)
                return NotFound();

            return Execute(() => _baseAplicacaoCroquiService.Inserir<AplicacaoCroquiValidator>(aplicacaoCroqui).Id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Entities.Entidades.Cadastros.Aplicacao.AplicacaoCroqui aplicacaoCroqui)
        {
            if (aplicacaoCroqui == null)
                return NotFound();

            return Execute(() => _baseAplicacaoCroquiService.Atualizar<AplicacaoCroquiValidator>(aplicacaoCroqui));
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _baseAplicacaoCroquiService.Remover(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Execute(() => _baseAplicacaoCroquiService.ListarTodasAplicacoesCroqui());

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

            return Execute(() => _baseAplicacaoCroquiService.BuscarPorId(id));
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
