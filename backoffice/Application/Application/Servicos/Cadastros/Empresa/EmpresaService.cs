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

namespace Application.Application.Servicos.Cadastros.Empresa;

public class EmpresaService : IEmpresaService
{
    private readonly IEmpresaRepository _empresaRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IUserAuthService _userAuthService;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;

    public EmpresaService(
        IMapper mapper, 
        IEmpresaRepository empresaRepository, 
        IUsuarioRepository usuarioRepository,
        IUserAuthService userAuthService, 
        IEmailService emailService
        )
    {
        _empresaRepository = empresaRepository;
        _usuarioRepository = usuarioRepository;
        _userAuthService = userAuthService;
        _mapper = mapper;
        _emailService = emailService;   
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
        if (empresaExist != null) 
        {
            resultMsg.Clear().Append("Empresa ja existe com este e-mail!");
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
                    var emailContent = new EmailViewModel
                    {
                        Recipient = empresaViewModel.Email,
                        Title = "Cadastre sua Senha",
                        Body = $"Você está acessando pela primeira vez como uma empresa cadastrada. Por favor clique no link abaixo para criar uma nova senha.",
                        Link = $"https://flytec-web.azurewebsites.net/primeiroAcessoEmpresa?token={HttpUtility.UrlEncode(message)}&email={HttpUtility.UrlEncode(empresaViewModel.Email)}",
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
        };

        return obj;
    }

    private IEnumerable<RoleObject> GenerateEmpresaUserRolesForViewModel()
    {
        var roles = Enumerable.Empty<RoleObject>();
        var roleObj = new RoleObject
        {
            Funcao = Domain.Enums.ERole.Administrador,
        };

        roles = roles.Append(roleObj);
        return roles;
    }

    public async Task UpdateAsync(EmpresaViewModel obj)
    {
        var mapEmpresa = _mapper.Map<Domain.Entidades.Cadastros.Empresa.Empresa>(obj);
        await _empresaRepository.UpdateAsync(mapEmpresa);
    }

    public async Task DeleteAsync(int id)
    {
        await _empresaRepository.DeleteAsync(id);
    }

    public async Task<string> GetLogoByIdAsync(int id)
    {
        var obj = await _empresaRepository.GetLogoByIdAsync(id);
        return obj;
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
