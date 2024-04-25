using Application.DTOs.Cadastros.Empresa.Interface;
using Application.DTOs.Cadastros.Empresa.ViewModel;
using Application.DTOs.Users.Interface;
using Application.DTOs.Users.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.Empresa;

namespace Application.Application.Servicos.Cadastros.Empresa;

public class EmpresaService : IEmpresaService
{
    private readonly IEmpresaRepository _empresaRepository;
    private readonly IUserAuthService _userAuthService;
    private readonly IMapper _mapper;

    public EmpresaService(IMapper mapper, IEmpresaRepository empresaRepository, IUserAuthService userAuthService)
    {
        _empresaRepository = empresaRepository;
        _userAuthService = userAuthService;
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

    public async Task AddAsync(EmpresaViewModel empresaViewModel)
    {
        var mapEmpresa = _mapper.Map<Domain.Entidades.Cadastros.Empresa.Empresa>(empresaViewModel);
        await _empresaRepository.AddAsync(mapEmpresa);
        var empresaUserObj = GenerateEmpresaUserObj(mapEmpresa);
        var createEmpresaUserOperationOk = await _userAuthService.RegisterUserFromEmpresaAsync(empresaUserObj, mapEmpresa.IdEmpresa);
        if (!createEmpresaUserOperationOk.Item1)
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
}
