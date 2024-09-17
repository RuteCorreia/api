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

    public async Task<IEnumerable<int>> GetDistinctBulaAsync(string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        return await _bulaRepository.GetDistinctBulaAsync(idEmpresaInt);
    }

    public async Task RemoveRecomendacaoAsync(int idBula)
    {
        await _bulaRepository.RemoveRecomendacaoAsync(idBula);
    }

    public async Task<BulaViewModel> GetByIdAsync(int id)
    {
        var obj = await _bulaRepository.GetByIdAsync(id);
        return _mapper.Map<BulaViewModel>(obj);
    }

    public async Task<BulaViewModel> GetByIdProdutoAsync(int idProduto)
    {
        var recomendacoesList = new List<RecomendacaoViewModel>();

        var bulas = await _bulaRepository.GetByIdProdutoAsync(idProduto);

        if (bulas == null || !bulas.Any())
        {
            return new BulaViewModel();
        }

        var bulaViewModel = new BulaViewModel
        {
            IdProduto = idProduto,
            Recomendacoes = recomendacoesList
        };

        foreach (var item in bulas)
        {
            var recomendacao = new RecomendacaoViewModel
            {
                IdBula = item.IdBula,
                IdCultura = item.IdCultura ?? 0, 
                IdAlvoBiologico = item.IdAlvoBiologico ?? 0, 
                DoseProdutoComercial = item.DoseProdutoComercial ?? string.Empty,
                IdTipoDeUnidade = item.IdTipoDeUnidade
            };

            recomendacoesList.Add(recomendacao);
        }
        bulaViewModel.Recomendacoes = recomendacoesList;

        return bulaViewModel;
    }

    public async Task AddAsync(BulaViewModel obj, string? idEmpresa)
    {
        foreach (var item in obj.Recomendacoes) 
        {
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var mapBula = _mapper.Map<Domain.Entidades.Cadastros.Empresa.Bula>(obj);
            mapBula.IdEmpresa = idEmpresaInt;
            mapBula.IdCultura = item.IdCultura;
            mapBula.IdAlvoBiologico = item.IdAlvoBiologico;
            mapBula.DoseProdutoComercial = item.DoseProdutoComercial;
            mapBula.IdTipoDeUnidade = item.IdTipoDeUnidade;
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
