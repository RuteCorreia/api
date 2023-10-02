using Domain.Interfaces.Cadastros.Empresa;
using Domain.Interfaces.Cadastros.PlanoContrato;
using Entities.Entidades.Cadastros.Empresa;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Validators;

namespace WebApp.Controllers.PlanoContrato
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanoContratoController : ControllerBase
    {
        private IPlanoContratoService _basePlanoContratoService;

        public PlanoContratoController(IPlanoContratoService basePlanoContratoService)
        {
            _basePlanoContratoService = basePlanoContratoService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] PlanoDeContrato planoDeContrato)
        {
            if (planoDeContrato == null)
                return NotFound();

            return Execute(() => _basePlanoContratoService.Inserir<PlanoContratoValidator>(planoDeContrato).IdPlano);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] PlanoDeContrato planoDeContrato)
        {
            if (planoDeContrato == null)
                return NotFound();

            return Execute(() => _basePlanoContratoService.Atualizar<PlanoContratoValidator>(planoDeContrato));
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _basePlanoContratoService.Remover(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Execute(() => _basePlanoContratoService.Listar());

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

            return Execute(() => _basePlanoContratoService.BuscarPorId(id));
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
