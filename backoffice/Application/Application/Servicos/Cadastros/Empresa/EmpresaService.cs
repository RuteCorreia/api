using Application.DTOs.Cadastros.Empresa.Interface;
using Application.DTOs.Cadastros.Empresa.ViewModel;
using Application.DTOs.Email.Interface;
using Application.DTOs.Email.ViewModel;
using Application.DTOs.Users.Interface;
using Application.DTOs.Users.ViewModel;
using AutoMapper;
using Domain.Enums;
using Domain.Interfaces.Cadastros.Empresa;
using System.Collections.Generic;
using Domain.Interfaces.User;
using System.Web;
using System.Text;
using Infra.Repositorio.User;
using Microsoft.AspNetCore.Identity;
using Azure.Core;
using Domain.Entidades.User;
using Application.DTOs.Cadastros.MenuUsuario.Interface;
using Domain.Entidades.Cadastros.Empresa;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace Application.Application.Servicos.Cadastros.Empresa;

public class EmpresaService : IEmpresaService
{
    private readonly IUsuarioCredencialRepository _usuarioCredencialRepository;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IEmpresaRepository _empresaRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IUserAuthService _userAuthService;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;

    public EmpresaService(
        IUsuarioCredencialRepository usuarioCredencialRepository,
        UserManager<IdentityUser> userManager, 
        IEmpresaRepository empresaRepository, 
        IUsuarioRepository usuarioRepository,
        IUserAuthService userAuthService,
        IConfiguration configuration,
        IEmailService emailService,
        IMapper mapper
        )
    {
        _usuarioCredencialRepository = usuarioCredencialRepository;
        _empresaRepository = empresaRepository;
        _usuarioRepository = usuarioRepository;
        _userAuthService = userAuthService;
        _configuration = configuration; 
        _emailService = emailService;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EmpresaViewModel>> GetAllAsync()
    {
        var list = await _empresaRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<EmpresaViewModel>>(list);
    }

    public async Task<EmpresaViewModel> GetByIdAsync(int id)
    {
        var obj = await _empresaRepository.GetByIdAsync(id);
        return _mapper.Map<EmpresaViewModel>(obj);
    }

    public async Task<(bool, string)> AddAsync(EmpresaViewModel empresaViewModel)
    {
        var resultMsg = new StringBuilder().Append("Envio de e-mail nao foi possivel, tente novamente");
        var mapEmpresa = _mapper.Map<Domain.Entidades.Cadastros.Empresa.Empresa>(empresaViewModel);
        var empresaExist = await _empresaRepository.GetByEmailAsync(mapEmpresa.Email);
        if (empresaExist != null && empresaExist.Removido)
        {
            resultMsg.Clear().Append("Encontramos uma empresa com esse email em nosso sistema! Deseja recuperala-lá?");
            return (false, resultMsg.ToString());
        }
        if (empresaExist != null && empresaExist.Removido == false) 
        {
            resultMsg.Clear().Append("Empresa ja existe com este e-mail e está ativa!");
            return (false, resultMsg.ToString());
        }
        await _empresaRepository.AddAsync(mapEmpresa);
        var empresaUserObj = GenerateEmpresaUserObj(mapEmpresa);
        var createEmpresaUserOperationOk = await _userAuthService.RegisterUserFromEmpresaAsync(empresaUserObj, mapEmpresa.IdEmpresa);
        var usuario = await _usuarioRepository.GetUserByEmailAsync(empresaUserObj.Email);
        if (createEmpresaUserOperationOk.Item1)
        {
            if (usuario.PrimeiroAcesso)
            {
                var (sucess, message) = await _emailService.GeneratePasswordResetTokenAsync(empresaViewModel.Email);

                if (sucess)
                {
                    var baseUrl = _configuration["AppSettings:UrlApi"];
                    var emailContent = new EmailViewModel
                    {
                        Recipient = empresaViewModel.Email,
                        Title = "Cadastre sua Senha",
                        Body = $"Você está acessando pela primeira vez como uma empresa cadastrada. Por favor clique no link abaixo para criar uma nova senha.",
                        Link = $"{baseUrl}primeiroAcessoEmpresa?token={HttpUtility.UrlEncode(message)}&email={HttpUtility.UrlEncode(empresaViewModel.Email)}",
                        LinkText = "Criar Nova Senha"
                    };

                    try
                    {
                        await _emailService.SendMailAsync(emailContent);
                        resultMsg.Clear().Append("E-mail de criação de senha de primeiro acesso enviado com sucesso.");
                        return (true, resultMsg.ToString());
                    }
                    catch (Exception ex)
                    {
                        resultMsg.Clear().Append("Erro ao enviar e-mail de de criação de senha, tente novamente");
                        return (false, resultMsg.ToString());
                    }
                }
                else
                {
                    resultMsg.Clear().Append("Usuario foi criado, mas tivemos um erro ao enviar o e-mail para criação de senha, por favor fale com o suporte");
                    return (false, resultMsg.ToString());
                }
            }
            else
            {
                resultMsg.Clear().Append("Empresa ja registrou primeiro acesso");
                return (false, resultMsg.ToString());
            }
        }
        else
        {
            await _empresaRepository.DeleteAsync(mapEmpresa.IdEmpresa);
            resultMsg.Clear().Append("Erro na criação do usuário da empresa. O cadastro não pôde ser realizado.");
            return (false, resultMsg.ToString());
        }



    }

    private UserRegisterViewModel GenerateEmpresaUserObj(Domain.Entidades.Cadastros.Empresa.Empresa empresa)
    {
        var obj = new UserRegisterViewModel
        {
            Name = $"Adm {empresa.Nome}",
            Email = empresa.Email,
            Password = $"Flytec_{DateTime.Now.Year}_!",
            Telefone = empresa.Telefone,
            CPF = "",
            Funcoes = GenerateEmpresaUserRolesForViewModel()
        };

        return obj;
    }

    private IEnumerable<RoleObject> GenerateEmpresaUserRolesForViewModel()
    {
        var roles = Enumerable.Empty<RoleObject>();
        var roleObj = new RoleObject
        {
            Funcao = Domain.Enums.ERole.Administrativo,
        };

        roles = roles.Append(roleObj);
        return roles;
    }

    private async Task CreateUserCredencial(IdentityUser user, IEnumerable<RoleObject> roles)
    {
        var usuario = await _usuarioRepository.GetByUserIdAsync(user.Id);
        var list = Enumerable.Empty<UsuarioCredencial>();
        foreach (var role in roles)
        {
            var obj = new UsuarioCredencial
            {
                IdUsuario = usuario.Id,
                Credencial = role.Credencial,
                Funcao = role.Funcao,
                NomeCompleto = role.NomeCompleto
            };

            list = list.Concat(new[] { obj });
        }

        await _usuarioCredencialRepository.AddListAsync(list);
    }
    public async Task<(bool, string)> UpdateAsync(EmpresaViewModel obj)
    {
        var resultMsg = new StringBuilder().Append("Atualização de usuário não foi possível");

        var userToUpdate = await _usuarioRepository.GetUserByEmpresaAndNameAsync(obj.IdEmpresa);
        if (userToUpdate != null)
        {
            var identityUser = await _userManager.FindByEmailAsync(userToUpdate.Email);
            if (identityUser != null)
            {
                bool allOk = true;

                var identityUserRoles = await _userManager.GetRolesAsync(identityUser);
                if (identityUser.Email != obj.Email)
                {
                    identityUser.Email = obj.Email;
                    var updateIdentityUserResult = await _userManager.UpdateAsync(identityUser);
                    if (!updateIdentityUserResult.Succeeded)
                    {
                        resultMsg.Append("Erro ao alterar nome de usuario");
                        allOk = false;
                    }
                }

                if (identityUser.PhoneNumber != obj.Telefone)
                {
                    identityUser.PhoneNumber = obj.Telefone;
                    var updateIdentityUserResult = await _userManager.UpdateAsync(identityUser);
                    if (!updateIdentityUserResult.Succeeded)
                    {
                        resultMsg.Append("Erro ao alterar nome de usuario");
                        allOk = false;
                    }
                }

                if (identityUser.UserName != obj.Email)
                {
                    identityUser.UserName = obj.Email;
                    var updateIdentityUserResult = await _userManager.UpdateAsync(identityUser);
                    if (!updateIdentityUserResult.Succeeded)
                    {
                        resultMsg.Append("Erro ao alterar nome de usuario");
                        allOk = false;
                    }
                }

                if (allOk)
                {
                    userToUpdate.Nome = $"Adm {obj.Nome}";
                    userToUpdate.Email = obj.Email;
                    userToUpdate.Telefone = obj.Telefone;
                    await _usuarioRepository.UpdateAsync(userToUpdate);
                    var mapEmpresa = _mapper.Map<Domain.Entidades.Cadastros.Empresa.Empresa>(obj);
                    await _empresaRepository.UpdateAsync(mapEmpresa);
                    resultMsg.Append("Sucesso na atualização do usuário");
                    return (true, resultMsg.ToString());
                }
            }
            else 
            {
                return (false, resultMsg.Clear().Append("O Email ja existe").ToString());
            }
        }
        return (false, resultMsg.ToString());
    }

    public async Task DeleteAsync(int id)
    {
        var usuarios = await _usuarioRepository.GetAllAsync(id);
        foreach (var usuario in usuarios) 
        {
            usuario.Removido = true;
            await _usuarioRepository.UpdateAsync(usuario);
        }
        await _empresaRepository.DeleteAsync(id);
    }

    public async Task<string> GetLogoByIdAsync(int id)
    {
        var obj = await _empresaRepository.GetLogoByIdAsync(id);
        return obj;
    }

    public async Task<EmpresaViewModel> GetByEmailAsync(string email)
    {
        var obj = await _empresaRepository.GetByEmailAsync(email);
        return _mapper.Map<EmpresaViewModel>(obj);
    }
    public async Task ChangeStatusAsync(int id, EStatusEmpresa status)
    {
        await _empresaRepository.ChangeStatusAsync(id, status);
    }

    public async Task<IEnumerable<EmpresaViewModel>> GetByNameAsync(string name)
    {
        var list = await _empresaRepository.GetByNameAsync(name);
        return _mapper.Map<IEnumerable<EmpresaViewModel>>(list);
    }
}
