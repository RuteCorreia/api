using Domain.Interfaces.Cadastros.CombateIncendioDecolagemPouso;
using Domain.Interfaces.Cadastros.ControleDeFrota;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Validators;

namespace WebApp.Controllers.ControleDeFrota
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControleDeFrotaController : ControllerBase
    {
        private IControleDeFrotaService _baseControleDeFrotaService;

        public ControleDeFrotaController(IControleDeFrotaService baseControleDeFrotaService)
        {
            _baseControleDeFrotaService = baseControleDeFrotaService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Entities.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota controleDeFrota)
        {
            if (controleDeFrota == null)
                return NotFound();

            return Execute(() => _baseControleDeFrotaService.Inserir<ControleDeFrotaValidator>(controleDeFrota).Id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Entities.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota controleDeFrota)
        {
            if (controleDeFrota == null)
                return NotFound();

            return Execute(() => _baseControleDeFrotaService.Atualizar<ControleDeFrotaValidator>(controleDeFrota));
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _baseControleDeFrotaService.Remover(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Execute(() => _baseControleDeFrotaService.Listar());

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

            return Execute(() => _baseControleDeFrotaService.BuscarPorId(id));
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
