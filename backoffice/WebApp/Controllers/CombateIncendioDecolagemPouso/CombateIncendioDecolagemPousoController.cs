using Domain.Interfaces.Cadastros.CombateIncendio;
using Domain.Interfaces.Cadastros.CombateIncendioDecolagemPouso;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Validators;

namespace WebApp.Controllers.CombateIncendioDecolagemPouso
{
    [Route("api/[controller]")]
    [ApiController]
    public class CombateIncendioDecolagemPousoController : ControllerBase
    {
        private ICombateIncendioDecolagemPousoService _baseCombateIncendioDecolagemPousoService;

        public CombateIncendioDecolagemPousoController(ICombateIncendioDecolagemPousoService baseCombateIncendioDecolagemPousoService)
        {
            _baseCombateIncendioDecolagemPousoService = baseCombateIncendioDecolagemPousoService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Entities.Entidades.Cadastros.CombateIncendio.CombateIncendioDecolagemPouso combateIncendioDecolagemPouso)
        {
            if (combateIncendioDecolagemPouso == null)
                return NotFound();

            return Execute(() => _baseCombateIncendioDecolagemPousoService.Inserir<CombateIncendioDecolagemPousoValidator>(combateIncendioDecolagemPouso).Id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Entities.Entidades.Cadastros.CombateIncendio.CombateIncendioDecolagemPouso combateIncendioDecolagemPouso)
        {
            if (combateIncendioDecolagemPouso == null)
                return NotFound();

            return Execute(() => _baseCombateIncendioDecolagemPousoService.Atualizar<CombateIncendioDecolagemPousoValidator>(combateIncendioDecolagemPouso));
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _baseCombateIncendioDecolagemPousoService.Remover(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Execute(() => _baseCombateIncendioDecolagemPousoService.Listar());

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

            return Execute(() => _baseCombateIncendioDecolagemPousoService.BuscarPorId(id));
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
