using System.Diagnostics;
using Application.Application.Servicos.Cadastros.Cliente;
using Application.DTOs.Cadastros.Adjuvante.Interface;
using Application.DTOs.Cadastros.Cliente.Interface;
using Application.DTOs.Cadastros.Engenheiro.Interface;
using Application.DTOs.Cadastros.Executor.Interface;
using Application.DTOs.Cadastros.Piloto.Interface;
using Domain.Entidades.Cadastros.Combustivel;
using Domain.Entidades.User;
using Microsoft.AspNetCore.Mvc;

namespace Backoffice.Controllers;

public class LoginController : Controller
{
    private readonly IClienteService _clienteService;
    private readonly IEngenheiroService _engenheiroService;
    private readonly IExecutorService _executorService;
    private readonly IPilotoService _pilotoService;

    public LoginController(IClienteService clienteService, IEngenheiroService engenheiroService, IExecutorService executorService, IPilotoService pilotoService)
    {
        _clienteService = clienteService;
        _engenheiroService = engenheiroService;
        _executorService = executorService;
        _pilotoService = pilotoService;
    }

    public IActionResult Entrar()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult<User>> Entrar(User request)
    {
        var cliente = await _clienteService.GetByLoginAsync(request.Username, request.Password);
        if (cliente != null)
        {
            var user = new User()
            {
                Username = cliente.NomeCliente,
                Role = "CLIENTE"
            };
            return Ok(user);
        }

        var engenheiro = await _engenheiroService.GetByLoginAsync(request.Username, request.Password);
        if (engenheiro != null)
        {
            var user = new User()
            {
                Username = engenheiro.Nome,
                Role = "ENGENHEIRO"
            };
            return Ok(user);
        }

        var executor = await _executorService.GetByLoginAsync(request.Username, request.Password);
        if (executor != null)
        {
            var user = new User()
            {
                Username = executor.Nome,
                Role = "EXECUTOR"
            };
            return Ok(user);
        }

        var piloto = await _pilotoService.GetByLoginAsync(request.Username, request.Password);
        if (piloto != null)
        {
            var user = new User()
            {
                Username = piloto.NomePiloto,
                Role = "PILOTO"
            };
            return Ok(user);
        }

        return new User();
    }
}
