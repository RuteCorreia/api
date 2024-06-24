using Application.DTOs.Cadastros.Empresa.Interface;
using Application.DTOs.Cadastros.Empresa.ViewModel;
using Application.DTOs.Email.Interface;
using Application.DTOs.Email.ViewModel;
using Application.DTOs.Users.Interface;
using Application.DTOs.Users.ViewModel;
using AutoMapper;
using Domain.Enums;
using Domain.Interfaces.Cadastros.Empresa;
using Domain.Interfaces.User;
using System.Web;

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

    public async Task AddAsync(EmpresaViewModel empresaViewModel)
    {
        var mapEmpresa = _mapper.Map<Domain.Entidades.Cadastros.Empresa.Empresa>(empresaViewModel);
        await _empresaRepository.AddAsync(mapEmpresa);
        var empresaUserObj = GenerateEmpresaUserObj(mapEmpresa);
        var createEmpresaUserOperationOk = await _userAuthService.RegisterUserFromEmpresaAsync(empresaUserObj, mapEmpresa.IdEmpresa);
        var usuario = await _usuarioRepository.GetUserByEmailAsync(empresaUserObj.Email);
        if (createEmpresaUserOperationOk.Item1)
        {
            if (usuario.PrimeiroAcesso)
            {
                var resetToken = await _emailService.GeneratePasswordResetTokenAsync(empresaViewModel.Email);

                if (resetToken != null)
                {
                    var emailContent = new EmailViewModel
                    {
                        Recipient = empresaViewModel.Email,
                        Title = "Cadastre sua Senha",
                        Body = $"Você está acessando pela primeira vez como uma empresa cadastrada. Por favor clique no link abaixo para criar uma nova senha.",
                        Link = $"https://flytec-web.azurewebsites.net/primeiroAcessoEmpresa?token={HttpUtility.UrlEncode(resetToken)}&email={HttpUtility.UrlEncode(empresaViewModel.Email)}",
                        LinkText = "Criar Nova Senha"
                    };

                    try
                    {
                        await _emailService.SendMailAsync(emailContent);
                        Console.WriteLine("E-mail de criação de senha de primeiro acesso enviado com sucesso.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao enviar e-mail de de criação de senha: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Erro ao gerar token de criação de senha.");
                }
            }
        }
        else
        {
            await _empresaRepository.DeleteAsync(mapEmpresa.IdEmpresa);
            throw new Exception("Erro na criação do usuário da empresa. O cadastro não pôde ser realizado.");
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
            Funcoes = GenerateEmpresaUserRolesForViewModel()
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
}
