using Application.DTOs.Cadastros.Cultura.Interface;
using Application.DTOs.Cadastros.Cultura.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.Cultura;
using Helpers;

namespace Application.Application.Servicos.Cadastros.Cultura;

public class CulturaService : ICulturaService
{
    private readonly ICulturaRepository _culturaRepository;
    private readonly IMapper _mapper;

    public CulturaService(IMapper mapper, ICulturaRepository culturaRepository)
    {
        _culturaRepository = culturaRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CulturaViewModel>> GetAllAsync(string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var list = await _culturaRepository.GetAllAsync(idEmpresaInt);
        return _mapper.Map<IEnumerable<CulturaViewModel>>(list);
    }

    public async Task<CulturaViewModel> GetByIdAsync(int id)
    {
        var obj = await _culturaRepository.GetByIdAsync(id);
        return _mapper.Map<CulturaViewModel>(obj);
    }

    public async Task AddAsync(CulturaViewModel obj, string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var mapCultura = _mapper.Map<Domain.Entidades.Cadastros.Cultura.Cultura>(obj);
        mapCultura.IdEmpresa = idEmpresaInt;
        await _culturaRepository.AddAsync(mapCultura);
    }

    public async Task UpdateAsync(CulturaViewModel obj)
    {
        var mapCultura = _mapper.Map<Domain.Entidades.Cadastros.Cultura.Cultura>(obj);
        await _culturaRepository.UpdateAsync(mapCultura);
    }

    public async Task DeleteAsync(int id)
    {
        await _culturaRepository.DeleteAsync(id);
    }

    public async Task<CulturaViewModel> GetByName(string name, string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var obj = await _culturaRepository.GetByNameAsync(name, idEmpresaInt);

        return _mapper.Map<CulturaViewModel>(obj);
    }
}
