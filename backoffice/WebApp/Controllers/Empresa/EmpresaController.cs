using Domain.Interfaces;
using Domain.Interfaces.Cadastros.Empresa;
using Entities.Entidades.Cadastros.Empresa;
using Entities.Entidades.User;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Validators;

namespace WebApp.Controllers.Empresa
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private IEmpresaService _baseEmpresaService;

        public EmpresaController(IEmpresaService baseUserService)
        {
            _baseEmpresaService = baseUserService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Entities.Entidades.Cadastros.Empresa.Empresa empresa)
        {
            if (empresa == null)
                return NotFound();

            return Execute(() => _baseEmpresaService.Inserir<EmpresaValidator>(empresa).IdEmpresa);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Entities.Entidades.Cadastros.Empresa.Empresa empresa)
        {
            if (empresa == null)
                return NotFound();

            return Execute(() => _baseEmpresaService.Atualizar<EmpresaValidator>(empresa));
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _baseEmpresaService.Remover(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Execute(() => _baseEmpresaService.Listar());

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

            return Execute(() => _baseEmpresaService.BuscarPorId(id));
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