using System.Diagnostics;
using Application.Application.Servicos.Cadastros.Cliente;
using Application.DTOs.Cadastros.Adjuvante.Interface;
using Application.DTOs.Cadastros.Cliente.Interface;
using Application.DTOs.Cadastros.Engenheiro.Interface;
using Application.DTOs.Cadastros.Executor.Interface;
using Application.DTOs.Cadastros.Piloto.Interface;
using Domain.Entidades.Cadastros.Combustivel;
using Domain.Entidades.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backoffice.Controllers;

public class LoginController : Controller
{
    private readonly string CookieLogin = "login-usuario";
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
        Response.Cookies.Delete(CookieLogin);
        return View();
    }

    [HttpPost]
    public async Task<ActionResult<User>> Entrar(User request)
    {
        var cliente = await _clienteService.GetByLoginAsync(request.Username, request.Password);
        if (cliente != null)
        {
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(7);
            var cookie = "cliente-" + cliente.IdCliente + "-" + cliente.NomeCliente;
            Response.Cookies.Append(CookieLogin, cookie, options);

            return Redirect("/");
        }

        var engenheiro = await _engenheiroService.GetByLoginAsync(request.Username, request.Password);
        if (engenheiro != null)
        {
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(7);
            var cookie = "engenheiro-" + engenheiro.IdEngenheiro + "-" + engenheiro.Nome;
            Response.Cookies.Append(CookieLogin, cookie, options);

            return Redirect("/");
        }

        var executor = await _executorService.GetByLoginAsync(request.Username, request.Password);
        if (executor != null)
        {
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(7);
            var cookie = "executor-" + executor.IdExecutor + "-" + executor.Nome;
            Response.Cookies.Append(CookieLogin, cookie, options);
        }

        var piloto = await _pilotoService.GetByLoginAsync(request.Username, request.Password);
        if (piloto != null)
        {
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(7);
            var cookie = "piloto-" + piloto.IdPiloto + "-" + piloto.NomePiloto;
            Response.Cookies.Append(CookieLogin, cookie, options);
        }

        return Redirect("/");
    }
}
