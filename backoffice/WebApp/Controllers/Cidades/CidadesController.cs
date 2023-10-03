using Domain.Interfaces.Cadastros.Bula;
using Domain.Interfaces.Cadastros.Cidades;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Validators;

namespace WebApp.Controllers.Cidades
{
    [Route("api/[controller]")]
    [ApiController]
    public class CidadesController : ControllerBase
    {
        private ICidadeService _baseCidadeService;

        public CidadesController(ICidadeService baseCidadeService)
        {
            _baseCidadeService = baseCidadeService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Entities.Entidades.Cadastros.Cidades.Cidades cidades)
        {
            if (cidades == null)
                return NotFound();

            return Execute(() => _baseCidadeService.Inserir<CidadeValidator>(cidades).Id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Entities.Entidades.Cadastros.Cidades.Cidades cidades)
        {
            if (cidades == null)
                return NotFound();

            return Execute(() => _baseCidadeService.Atualizar<CidadeValidator>(cidades));
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _baseCidadeService.Remover(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Execute(() => _baseCidadeService.Listar());

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

            return Execute(() => _baseCidadeService.BuscarPorId(id));
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
