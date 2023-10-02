using Domain.Interfaces.Cadastros.Executor;
using Domain.Interfaces.Cadastros.Frota;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Validators;

namespace WebApp.Controllers.Frota
{
    [Route("api/[controller]")]
    [ApiController]
    public class FrotaController : ControllerBase
    {
        private IFrotaService _baseFrotaService;

        public FrotaController(IFrotaService baseFrotaService)
        {
            _baseFrotaService = baseFrotaService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Entities.Entidades.Cadastros.Frota.Frota frota)
        {
            if (frota == null)
                return NotFound();

            return Execute(() => _baseFrotaService.Inserir<FrotaValidator>(frota).Id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Entities.Entidades.Cadastros.Frota.Frota frota)
        {
            if (frota == null)
                return NotFound();

            return Execute(() => _baseFrotaService.Atualizar<FrotaValidator>(frota));
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _baseFrotaService.Remover(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Execute(() => _baseFrotaService.Listar());

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

            return Execute(() => _baseFrotaService.BuscarPorId(id));
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
