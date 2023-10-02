using Domain.Interfaces.Cadastros.Engenheiro;
using Domain.Interfaces.Cadastros.Piloto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Validators;

namespace WebApp.Controllers.Engenheiro
{
    public class EngenheiroController : ControllerBase
    {
        private IEngenheiroService _baseEngenheiroService;

        public EngenheiroController(IEngenheiroService baseEngenheiroService)
        {
            _baseEngenheiroService = baseEngenheiroService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Entities.Entidades.Cadastros.Engenheiros.Engenheiro engenheiro)
        {
            if (engenheiro == null)
                return NotFound();

            return Execute(() => _baseEngenheiroService.Inserir<EngenheiroValidator>(engenheiro).IdEngenheiro);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Entities.Entidades.Cadastros.Engenheiros.Engenheiro engenheiro)
        {
            if (engenheiro == null)
                return NotFound();

            return Execute(() => _baseEngenheiroService.Atualizar<EngenheiroValidator>(engenheiro));
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _baseEngenheiroService.Remover(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Execute(() => _baseEngenheiroService.Listar());

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

            return Execute(() => _baseEngenheiroService.BuscarPorId(id));
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
