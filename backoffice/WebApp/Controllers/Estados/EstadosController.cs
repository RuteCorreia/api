using Domain.Interfaces.Cadastros.Cidades;
using Domain.Interfaces.Cadastros.Estados;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Validators;

namespace WebApp.Controllers.Estados
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadosController : ControllerBase
    {
        private IEstadosService _baseEstadosService;

        public EstadosController(IEstadosService baseEstadosService)
        {
            _baseEstadosService = baseEstadosService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Entities.Entidades.Cadastros.Estados.Estados estados)
        {
            if (estados == null)
                return NotFound();

            return Execute(() => _baseEstadosService.Inserir<EstadoValidator>(estados).Id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Entities.Entidades.Cadastros.Estados.Estados estados)
        {
            if (estados == null)
                return NotFound();

            return Execute(() => _baseEstadosService.Atualizar<EstadoValidator>(estados));
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _baseEstadosService.Remover(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Execute(() => _baseEstadosService.Listar());

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

            return Execute(() => _baseEstadosService.BuscarPorId(id));
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
