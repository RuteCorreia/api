using Domain.Interfaces.Cadastros.Cliente;
using Entities.Entidades.Cadastros.Cliente;
using Entities.Entidades.Cadastros.Empresa;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Validators;

namespace WebApp.Controllers.Cliente
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, ResponsavelEmpresa")]
    public class ClienteController : ControllerBase
    {
        private IClienteService _baseClienteService;

        public ClienteController(IClienteService baseClienteService)
        {
            _baseClienteService = baseClienteService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Entities.Entidades.Cadastros.Cliente.Cliente cliente)
        {
            if (cliente == null)
                return NotFound();

            return Execute(() => _baseClienteService.Inserir<ClienteValidator>(cliente).IdCliente);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Entities.Entidades.Cadastros.Cliente.Cliente cliente)
        {
            if (cliente == null)
                return NotFound();

            return Execute(() => _baseClienteService.Atualizar<ClienteValidator>(cliente));
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _baseClienteService.Remover(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Execute(() => _baseClienteService.Listar());

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

            return Execute(() => _baseClienteService.BuscarPorId(id));
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
