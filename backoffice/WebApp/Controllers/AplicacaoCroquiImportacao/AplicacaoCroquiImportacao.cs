using Domain.Interfaces.Cadastros.AplicacaoCroqui;
using Domain.Interfaces.Cadastros.AplicacaoCroquiImportacao;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Validators;

namespace WebApp.Controllers.AplicacaoCroquiImportacao
{
    [Route("api/[controller]")]
    [ApiController]
    public class AplicacaoCroquiImportacao : ControllerBase
    {
        private IAplicacaoCroquiImportacaoService _baseAplicacaoCroquiImportacaoService;

        public AplicacaoCroquiImportacao(IAplicacaoCroquiImportacaoService baseAplicacaoCroquiImportacaoService)
        {
            _baseAplicacaoCroquiImportacaoService = baseAplicacaoCroquiImportacaoService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Entities.Entidades.Cadastros.Aplicacao.AplicacaoCroquiImportacao aplicacaoCroquiImportacao)
        {
            if (aplicacaoCroquiImportacao == null)
                return NotFound();

            return Execute(() => _baseAplicacaoCroquiImportacaoService.Inserir<AplicacaoCroquiImportacaoValidator>(aplicacaoCroquiImportacao).Id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Entities.Entidades.Cadastros.Aplicacao.AplicacaoCroquiImportacao aplicacaoCroquiImportacao)
        {
            if (aplicacaoCroquiImportacao == null)
                return NotFound();

            return Execute(() => _baseAplicacaoCroquiImportacaoService.Atualizar<AplicacaoCroquiImportacaoValidator>(aplicacaoCroquiImportacao));
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _baseAplicacaoCroquiImportacaoService.Remover(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Execute(() => _baseAplicacaoCroquiImportacaoService.ListarTodasAplicacoesCroquiImportacoes());

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

            return Execute(() => _baseAplicacaoCroquiImportacaoService.BuscarPorId(id));
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
