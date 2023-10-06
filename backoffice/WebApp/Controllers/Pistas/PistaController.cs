using Domain.Interfaces.Cadastros.Pista;
using Domain.Interfaces.Cadastros.Precificacao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Validators;

namespace WebApp.Controllers.Pistas
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PistaController : ControllerBase
    {
        private IPistaService _basePistaService;

        public PistaController(IPistaService basePistaService)
        {
            _basePistaService = basePistaService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Entities.Entidades.Cadastros.Pistas.Pista pista)
        {
            if (pista == null)
                return NotFound();

            return Execute(() => _basePistaService.Inserir<PistaValidator>(pista).Id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Entities.Entidades.Cadastros.Pistas.Pista pista)
        {
            if (pista == null)
                return NotFound();

            return Execute(() => _basePistaService.Atualizar<PistaValidator>(pista));
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _basePistaService.Remover(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Execute(() => _basePistaService.Listar());

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

            return Execute(() => _basePistaService.BuscarPorId(id));
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
