using Application.DTOs.Cadastros.Empresa.Interface;
using Application.DTOs.Cadastros.Empresa.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.Empresa;

namespace Application.Application.Servicos.Cadastros.Empresa;

public class EmpresaService : IEmpresaService
{
    private readonly IEmpresaRepository _empresaRepository;
    private readonly IMapper _mapper;

    public EmpresaService(IMapper mapper, IEmpresaRepository empresaRepository)
    {
        _empresaRepository = empresaRepository;
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
