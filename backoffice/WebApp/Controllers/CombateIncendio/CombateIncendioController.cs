using Domain.Interfaces.Cadastros.Cliente;
using Domain.Interfaces.Cadastros.CombateIncendio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Validators;

namespace WebApp.Controllers.CombateIncendio
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CombateIncendioController : ControllerBase
    {
        private ICombateIncendioService _baseCombateIncendioService;

        public CombateIncendioController(ICombateIncendioService baseCombateIncendioService)
        {
            _baseCombateIncendioService = baseCombateIncendioService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Entities.Entidades.Cadastros.CombateIncendio.CombateIncendio combateIncendio)
        {
            if (combateIncendio == null)
                return NotFound();

            return Execute(() => _baseCombateIncendioService.Inserir<CombateIncendioValidator>(combateIncendio).Id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Entities.Entidades.Cadastros.CombateIncendio.CombateIncendio combateIncendio)
        {
            if (combateIncendio == null)
                return NotFound();

            return Execute(() => _baseCombateIncendioService.Atualizar<CombateIncendioValidator>(combateIncendio));
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _baseCombateIncendioService.Remover(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Execute(() => _baseCombateIncendioService.ListarTodosCombatesIncendio());

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

            return Execute(() => _baseCombateIncendioService.BuscarPorId(id));
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
