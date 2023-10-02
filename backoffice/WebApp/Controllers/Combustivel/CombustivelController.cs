using Domain.Interfaces.Cadastros.Combustivel;
using Domain.Interfaces.Cadastros.Engenheiro;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Validators;

namespace WebApp.Controllers.Combustivel
{
    [Route("api/[controller]")]
    [ApiController]
    public class CombustivelController : ControllerBase
    {
        private ICombustivelService _baseCombustivelService;

        public CombustivelController(ICombustivelService baseCombustivelService)
        {
            _baseCombustivelService = baseCombustivelService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Entities.Entidades.Cadastros.Combustivel.Combustivel combustivel)
        {
            if (combustivel == null)
                return NotFound();

            return Execute(() => _baseCombustivelService.Inserir<CombustivelValidator>(combustivel).Id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Entities.Entidades.Cadastros.Combustivel.Combustivel combustivel)
        {
            if (combustivel == null)
                return NotFound();

            return Execute(() => _baseCombustivelService.Atualizar<CombustivelValidator>(combustivel));
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _baseCombustivelService.Remover(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Execute(() => _baseCombustivelService.Listar());

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

            return Execute(() => _baseCombustivelService.BuscarPorId(id));
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
