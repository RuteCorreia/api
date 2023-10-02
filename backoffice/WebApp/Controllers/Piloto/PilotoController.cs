using Domain.Interfaces.Cadastros.Piloto;
using Domain.Interfaces.Cadastros.PlanoContrato;
using Entities.Entidades.Cadastros.Empresa;
using Entities.Entidades.Cadastros.Pilotos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Validators;

namespace WebApp.Controllers.Piloto
{
    [Route("api/[controller]")]
    [ApiController]
    public class PilotoController : ControllerBase
    {
        private IPilotoService _basePilotoService;

        public PilotoController(IPilotoService basePilotoService)
        {
            _basePilotoService = basePilotoService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Entities.Entidades.Cadastros.Pilotos.Piloto piloto)
        {
            if (piloto == null)
                return NotFound();

            return Execute(() => _basePilotoService.Inserir<PilotoValidator>(piloto).IdPiloto);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Entities.Entidades.Cadastros.Pilotos.Piloto piloto)
        {
            if (piloto == null)
                return NotFound();

            return Execute(() => _basePilotoService.Atualizar<PilotoValidator>(piloto));
        }

        [HttpDelete("{id}")]
        public IActionResult RemoverEngenheiro(int id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _basePilotoService.Remover(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Execute(() => _basePilotoService.Listar());

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

            return Execute(() => _basePilotoService.BuscarPorId(id));
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
