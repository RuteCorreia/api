using Domain.Interfaces.Cadastros.AlvoBiologico;
using Domain.Interfaces.Cadastros.Bula;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Validators;

namespace WebApp.Controllers.AlvoBiologico
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlvoBiologicoController : ControllerBase
    {
        private IAlvoBiologicoService _baseAlvoBiologicoService;

        public AlvoBiologicoController(IAlvoBiologicoService baseAlvoBiologicoService)
        {
            _baseAlvoBiologicoService = baseAlvoBiologicoService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Entities.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico alvoBiologico)
        {
            if (alvoBiologico == null)
                return NotFound();

            return Execute(() => _baseAlvoBiologicoService.Inserir<AlvoBiologicoValidator>(alvoBiologico).Id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Entities.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico alvoBiologico)
        {
            if (alvoBiologico == null)
                return NotFound();

            return Execute(() => _baseAlvoBiologicoService.Atualizar<AlvoBiologicoValidator>(alvoBiologico));
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _baseAlvoBiologicoService.Remover(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Execute(() => _baseAlvoBiologicoService.ListarTodosAlvosBiologicos());

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

            return Execute(() => _baseAlvoBiologicoService.BuscarPorId(id));
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
