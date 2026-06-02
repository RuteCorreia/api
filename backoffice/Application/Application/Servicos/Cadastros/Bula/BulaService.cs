using Application.DTOs.Cadastros.Bula.Interface;
using Application.DTOs.Cadastros.Bula.ViewModel;
using AutoMapper;
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

    public async Task<IEnumerable<BulaAppViewModel>> GetAllAsync(string? idEmpresa)
    {
        var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
        var bulas = await _bulaRepository.GetAllAsync(idEmpresaInt);
        var bulaViewModel =  _mapper.Map<IEnumerable<BulaAppViewModel>>(bulas);
        return bulaViewModel;
    }

    public async Task<IEnumerable<(int, int)>> GetDistinctBulaAsync(string? idEmpresa, string? nomeProduto)
    {
        var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
        return await _bulaRepository.GetDistinctBulaAsync(idEmpresaInt, nomeProduto);
    }

    public async Task RemoveRecomendacaoAsync(int idBula, string? idEmpresa)
    {
        var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
        await _bulaRepository.RemoveRecomendacaoAsync(idBula, idEmpresaInt);
    }

    public async Task RemoveBulaAsync(int idProduto, string? idEmpresa)
    {
        var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
        await _bulaRepository.RemoveBulaAsync(idProduto, idEmpresaInt);
    }

    public async Task<BulaViewModel> GetByIdAsync(int id, string? idEmpresa)
    {
        var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
        var obj = await _bulaRepository.GetByIdAsync(id, idEmpresaInt);
        return _mapper.Map<BulaViewModel>(obj);
    }

    public async Task<BulaViewModel> GetByIdProdutoAsync(int idProduto, string? idEmpresa)
    {
        var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
        var recomendacoesList = new List<RecomendacaoViewModel>();

        var bulas = await _bulaRepository.GetByIdProdutoAsync(idProduto, idEmpresaInt);

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
                IdCultura = item.IdCultura, 
                IdAlvoBiologico = item.IdAlvoBiologico, 
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
            var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
            var mapBula = _mapper.Map<Domain.Entidades.Cadastros.Empresa.Bula>(obj);
            mapBula.IdEmpresa = idEmpresaInt;
            mapBula.IdCultura = item.IdCultura == 0 ? null : item.IdCultura;
            mapBula.IdAlvoBiologico = item.IdAlvoBiologico == 0 ? null : item.IdAlvoBiologico;
            mapBula.DoseProdutoComercial = item.DoseProdutoComercial;
            mapBula.IdTipoDeUnidade = item.IdTipoDeUnidade;
            if (item.IdBula == null || item.IdBula == 0)
            {
                await _bulaRepository.AddAsync(mapBula);
            }
            else if (item.IdBula > 0) 
            {
                mapBula.IdBula = item.IdBula ?? 0;
                await _bulaRepository.UpdateAsync(mapBula);
            }
            else
            {
                throw new Exception("nao foi possivel adicionar a bula");
            }     
        }
    }

    public async Task UpdateAsync(BulaViewModel obj)
    {
        var mapBula = _mapper.Map<Domain.Entidades.Cadastros.Empresa.Bula>(obj);
        await _bulaRepository.UpdateAsync(mapBula);
    }

    public async Task DeleteAsync(int id, string? idEmpresa)
    {
        var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
        await _bulaRepository.DeleteAsync(id, idEmpresaInt);
    }

    public async Task<BulaViewModel> GetByName(string name, string? idEmpresa)
    {
        var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
        var obj = await _bulaRepository.GetByNameAsync(name, idEmpresaInt);

        return _mapper.Map<BulaViewModel>(obj);
    }
}
