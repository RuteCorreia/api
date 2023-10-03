using Domain.Interfaces.Cadastros.AplicacaoRelatorio;
using Domain.Interfaces.Cadastros.AplicacaoRelatorioItem;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Validators;

namespace WebApp.Controllers.AplicacaoRelatorioItem
{
    [Route("api/[controller]")]
    [ApiController]
    public class AplicacaoRelatorioItemController : ControllerBase
    {
        private IAplicacaoRelatorioItemService _baseAplicacaoRelatorioItemService;

        public AplicacaoRelatorioItemController(IAplicacaoRelatorioItemService baseAplicacaoRelatorioItemService)
        {
            _baseAplicacaoRelatorioItemService = baseAplicacaoRelatorioItemService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Entities.Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem aplicacaoRelatorioItem)
        {
            if (aplicacaoRelatorioItem == null)
                return NotFound();

            return Execute(() => _baseAplicacaoRelatorioItemService.Inserir<AplicacaoRelatorioItemValidator>(aplicacaoRelatorioItem).Id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Entities.Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem aplicacaoRelatorioItem)
        {
            if (aplicacaoRelatorioItem == null)
                return NotFound();

            return Execute(() => _baseAplicacaoRelatorioItemService.Atualizar<AplicacaoRelatorioItemValidator>(aplicacaoRelatorioItem));
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _baseAplicacaoRelatorioItemService.Remover(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Execute(() => _baseAplicacaoRelatorioItemService.Listar());

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

            return Execute(() => _baseAplicacaoRelatorioItemService.BuscarPorId(id));
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
