using Domain.Interfaces.Cadastros.AlvoBiologico;
using Domain.Interfaces.Cadastros.Aplicacao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Validators;

namespace WebApp.Controllers.Aplicacao
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AplicacaoController : ControllerBase
    {
        private IAplicacaoService _baseAplicacaoService;

        public AplicacaoController(IAplicacaoService baseAplicacaoService)
        {
            _baseAplicacaoService = baseAplicacaoService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Entities.Entidades.Cadastros.Aplicacao.Aplicacao aplicacao)
        {
            if (aplicacao == null)
                return NotFound();

            return Execute(() => _baseAplicacaoService.Inserir<AplicacaoValidator>(aplicacao).Id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Entities.Entidades.Cadastros.Aplicacao.Aplicacao aplicacao)
        {
            if (aplicacao == null)
                return NotFound();

            return Execute(() => _baseAplicacaoService.Atualizar<AplicacaoValidator>(aplicacao));
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _baseAplicacaoService.Remover(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Execute(() => _baseAplicacaoService.ListarTodasAplicacoes());

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

            return Execute(() => _baseAplicacaoService.BuscarPorId(id));
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
