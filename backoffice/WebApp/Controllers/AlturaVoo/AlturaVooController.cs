using Domain.Interfaces.Cadastros.Adjuvante;
using Domain.Interfaces.Cadastros.AlturaVoo;
using Entities.Entidades.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Validators;

namespace WebApp.Controllers.AlturaVoo
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AlturaVooController : ControllerBase
    {
        private IAlturaVooService _baseAlturaVooService;

        public AlturaVooController(IAlturaVooService baseAlturaVooService)
        {
            _baseAlturaVooService = baseAlturaVooService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Entities.Entidades.Cadastros.Altura_Voo.AlturaVoo alturaVoo)
        {
            if (alturaVoo == null)
                return NotFound();

            return Execute(() => _baseAlturaVooService.Inserir<AlturaVooValidator>(alturaVoo).Id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Entities.Entidades.Cadastros.Altura_Voo.AlturaVoo alturaVoo)
        {
            if (alturaVoo == null)
                return NotFound();

            return Execute(() => _baseAlturaVooService.Atualizar<AlturaVooValidator>(alturaVoo));
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _baseAlturaVooService.Remover(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Execute(() => _baseAlturaVooService.Listar());

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

            return Execute(() => _baseAlturaVooService.BuscarPorId(id));
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
