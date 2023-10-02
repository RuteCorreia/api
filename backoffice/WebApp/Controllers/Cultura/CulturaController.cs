using Domain.Interfaces.Cadastros.Cultura;
using Domain.Interfaces.Cadastros.Executor;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Validators;

namespace WebApp.Controllers.Cultura
{
    [Route("api/[controller]")]
    [ApiController]
    public class CulturaController : ControllerBase
    {
        private ICulturaService _baseCulturaService;

        public CulturaController(ICulturaService baseCulturaService)
        {
            _baseCulturaService = baseCulturaService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Entities.Entidades.Cadastros.Cultura.Cultura cultura)
        {
            if (cultura == null)
                return NotFound();

            return Execute(() => _baseCulturaService.Inserir<CulturaValidator>(cultura).IdCultura);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Entities.Entidades.Cadastros.Cultura.Cultura cultura)
        {
            if (cultura == null)
                return NotFound();

            return Execute(() => _baseCulturaService.Atualizar<CulturaValidator>(cultura));
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _baseCulturaService.Remover(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Execute(() => _baseCulturaService.Listar());

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

            return Execute(() => _baseCulturaService.BuscarPorId(id));
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