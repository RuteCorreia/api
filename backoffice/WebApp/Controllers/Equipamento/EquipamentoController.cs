using Domain.Interfaces.Cadastros.Engenheiro;
using Domain.Interfaces.Cadastros.Equipamento;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Validators;

namespace WebApp.Controllers.Equipamento
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipamentoController : ControllerBase
    {
        private IEquipamentoService _baseEquipamentoService;

        public EquipamentoController(IEquipamentoService baseEquipamentoService)
        {
            _baseEquipamentoService = baseEquipamentoService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Entities.Entidades.Cadastros.Equipamento.Equipamento equipamento)
        {
            if (equipamento == null)
                return NotFound();

            return Execute(() => _baseEquipamentoService.Inserir<EquipamentoValidator>(equipamento).Id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Entities.Entidades.Cadastros.Equipamento.Equipamento equipamento)
        {
            if (equipamento == null)
                return NotFound();

            return Execute(() => _baseEquipamentoService.Atualizar<EquipamentoValidator>(equipamento));
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _baseEquipamentoService.Remover(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Execute(() => _baseEquipamentoService.Listar());

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

            return Execute(() => _baseEquipamentoService.BuscarPorId(id));
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
