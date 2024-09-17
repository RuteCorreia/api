using Application.DTOs.Cadastros.Bula.Interface;
using Application.DTOs.Cadastros.Bula.ViewModel;
using Application.DTOs.Cadastros.Cultura.ViewModel;
using AutoMapper;
using Domain.Entidades.Cadastros.Empresa;
using Domain.Interfaces.Cadastros.Bula;
using Helpers;

namespace Application.Application.Servicos.Cadastros.Bula;

public class BulaService : IBulaService
{
    private readonly IBulaRepository _bulaRepository;
    private readonly IMapper _mapper;

    public BulaService(IMapper mapper, IBulaRepository bulaRepository)
    {
        _bulaRepository = bulaRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BulaViewModel>> GetAllAsync(string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var list = await _bulaRepository.GetAllAsync(idEmpresaInt);
        return _mapper.Map<IEnumerable<BulaViewModel>>(list);
    }

    public async Task<BulaViewModel> GetByIdAsync(int id)
    {
        var obj = await _bulaRepository.GetByIdAsync(id);
        return _mapper.Map<BulaViewModel>(obj);
    }

    public async Task AddAsync(BulaViewModel obj)
    {
        foreach (var item in obj.Recomendacoes) 
        {
            var mapBula = _mapper.Map<Domain.Entidades.Cadastros.Empresa.Bula>(obj);
            mapBula.IdProduto = obj.IdProduto;
            mapBula.IdCultura = item.IdCultura;
            mapBula.IdAlvoBiologico = item.IdAlvoBiologico;
            mapBula.DoseProdutoComercial = item.DoseProdutoComercial;
            mapBula.UnidadeProduto = item.UnidadeProduto;
            await _bulaRepository.AddAsync(mapBula);
        }
    }

    public async Task UpdateAsync(BulaViewModel obj)
    {
        var mapBula = _mapper.Map<Domain.Entidades.Cadastros.Empresa.Bula>(obj);
        await _bulaRepository.UpdateAsync(mapBula);
    }

    public async Task DeleteAsync(int id)
    {
        await _bulaRepository.DeleteAsync(id);
    }

    public async Task<BulaViewModel> GetByName(string name, string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var obj = await _bulaRepository.GetByNameAsync(name, idEmpresaInt);

        return _mapper.Map<BulaViewModel>(obj);
    }
}
