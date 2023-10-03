using Domain.Interfaces.Cadastros.Produto;
using Domain.Interfaces.Cadastros.TipoProduto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Validators;

namespace WebApp.Controllers.TipoProduto
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoProdutoController : ControllerBase
    {
        private ITipoProdutoService _baseTipoProdutoService;

        public TipoProdutoController(ITipoProdutoService baseTipoProdutoService)
        {
            _baseTipoProdutoService = baseTipoProdutoService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Entities.Entidades.Cadastros.Tipo_Produto.TipoProduto tipoProduto)
        {
            if (tipoProduto == null)
                return NotFound();

            return Execute(() => _baseTipoProdutoService.Inserir<TipoProdutoValidator>(tipoProduto).Id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Entities.Entidades.Cadastros.Tipo_Produto.TipoProduto tipoProduto)
        {
            if (tipoProduto == null)
                return NotFound();

            return Execute(() => _baseTipoProdutoService.Atualizar<TipoProdutoValidator>(tipoProduto));
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _baseTipoProdutoService.Remover(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Execute(() => _baseTipoProdutoService.Listar());

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

            return Execute(() => _baseTipoProdutoService.BuscarPorId(id));
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
