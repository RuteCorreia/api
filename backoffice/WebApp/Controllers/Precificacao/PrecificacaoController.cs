using Domain.Interfaces.Cadastros.Engenheiro;
using Domain.Interfaces.Cadastros.Precificacao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Validators;

namespace WebApp.Controllers.Precificacao
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PrecificacaoController : ControllerBase
    {
        private IPrecificacaoService _basePrecificacaoService;

        public PrecificacaoController(IPrecificacaoService basePrecificacaoService)
        {
            _basePrecificacaoService = basePrecificacaoService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Entities.Entidades.Cadastros.Precificacao.Precificacao precificacao)
        {
            if (precificacao == null)
                return NotFound();

            return Execute(() => _basePrecificacaoService.Inserir<PrecificacaoValidator>(precificacao).Id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Entities.Entidades.Cadastros.Precificacao.Precificacao precificacao)
        {
            if (precificacao == null)
                return NotFound();

            return Execute(() => _basePrecificacaoService.Atualizar<PrecificacaoValidator>(precificacao));
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _basePrecificacaoService.Remover(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Execute(() => _basePrecificacaoService.Listar());

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

            return Execute(() => _basePrecificacaoService.BuscarPorId(id));
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
