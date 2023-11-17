using Domain.Interfaces.Cadastros.Piloto;
using Domain.Interfaces.Cadastros.Veiculante;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Validators;

namespace WebApp.Controllers.Veiculante
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VeiculanteController : ControllerBase
    {
        private IVeiculanteService _baseVeiculanteService;

        public VeiculanteController(IVeiculanteService baseVeiculanteService)
        {
            _baseVeiculanteService = baseVeiculanteService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Entities.Entidades.Cadastros.Veiculante.Veiculante veiculante)
        {
            if (veiculante == null)
                return NotFound();

            return Execute(() => _baseVeiculanteService.Inserir<VeiculanteValidator>(veiculante).IdVeiculante);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Entities.Entidades.Cadastros.Veiculante.Veiculante veiculante)
        {
            if (veiculante == null)
                return NotFound();

            return Execute(() => _baseVeiculanteService.Atualizar<VeiculanteValidator>(veiculante));
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _baseVeiculanteService.Remover(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Execute(() => _baseVeiculanteService.Listar());

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

            return Execute(() => _baseVeiculanteService.BuscarPorId(id));
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
