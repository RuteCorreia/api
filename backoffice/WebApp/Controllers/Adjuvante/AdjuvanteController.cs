using Domain.Interfaces.Cadastros.Adjuvante;
using Domain.Interfaces.Cadastros.Aeronave;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Validators;

namespace WebApp.Controllers.Adjuvante
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdjuvanteController : ControllerBase
    {
        private IAdjuvanteService _baseAdjuvanteService;

        public AdjuvanteController(IAdjuvanteService baseAdjuvanteService)
        {
            _baseAdjuvanteService = baseAdjuvanteService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Entities.Entidades.Cadastros.Adjuvante.Adjuvante adjuvante)
        {
            if (adjuvante == null)
                return NotFound();

            return Execute(() => _baseAdjuvanteService.Inserir<AdjuvanteValidator>(adjuvante).Id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Entities.Entidades.Cadastros.Adjuvante.Adjuvante adjuvante)
        {
            if (adjuvante == null)
                return NotFound();

            return Execute(() => _baseAdjuvanteService.Atualizar<AdjuvanteValidator>(adjuvante));
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _baseAdjuvanteService.Remover(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Execute(() => _baseAdjuvanteService.Listar());

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

            return Execute(() => _baseAdjuvanteService.BuscarPorId(id));
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
