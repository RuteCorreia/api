using Domain.Interfaces.Cadastros.Aeronave;
using Domain.Interfaces.Cadastros.Frota;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Validators;

namespace WebApp.Controllers.Aeronave
{
    [Route("api/[controller]")]
    [ApiController]
    public class AeronaveController : ControllerBase
    {
        private IAeronaveService _baseAeronaveService;

        public AeronaveController(IAeronaveService baseAeronaveService)
        {
            _baseAeronaveService = baseAeronaveService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Entities.Entidades.Cadastros.Aeronaves.Aeronave aeronave)
        {
            if (aeronave == null)
                return NotFound();

            return Execute(() => _baseAeronaveService.Inserir<AeronaveValidator>(aeronave).Id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Entities.Entidades.Cadastros.Aeronaves.Aeronave aeronave)
        {
            if (aeronave == null)
                return NotFound();

            return Execute(() => _baseAeronaveService.Atualizar<AeronaveValidator>(aeronave));
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _baseAeronaveService.Remover(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Execute(() => _baseAeronaveService.Listar());

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

            return Execute(() => _baseAeronaveService.BuscarPorId(id));
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
