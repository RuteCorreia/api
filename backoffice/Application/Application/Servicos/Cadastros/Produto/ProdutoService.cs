using Application.DTOs.Cadastros.Produto.Interface;
using Application.DTOs.Cadastros.Produto.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.Produto;
using Helpers;

namespace Application.Application.Servicos.Cadastros.Produto;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IMapper _mapper;

    public ProdutoService(IMapper mapper, IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProdutoViewModel>> GetAllAsync(string? nomeProduto, string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var list = await _produtoRepository.GetAllAsync(nomeProduto, idEmpresaInt);
        return _mapper.Map<IEnumerable<ProdutoViewModel>>(list);
    }

    public async Task<IEnumerable<ProdutoViewModel>> GetAllAppAsync(string? nomeProduto, string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var list = await _produtoRepository.GetAllAsync(nomeProduto, idEmpresaInt);
        foreach (var item in list)
        {
            if (!string.IsNullOrEmpty(item.ClassificacaoToxicologica) && item.ClassificacaoToxicologica.Contains("Categoria"))
            {
                var categoria = item.ClassificacaoToxicologica.Split('-')[0]
                               .Replace("Categoria", "").Trim();

                item.ClassificacaoToxicologica = categoria;
            }
        }
        return _mapper.Map<IEnumerable<ProdutoViewModel>>(list);
    }

    public async Task<ProdutoViewModel> GetByIdAsync(int id, string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var obj = await _produtoRepository.GetByIdAsync(id, idEmpresaInt);
        return _mapper.Map<ProdutoViewModel>(obj);
    }

    public async Task<ProdutoViewModel> GetByIdAppAsync(int id, string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var obj = await _produtoRepository.GetByIdAsync(id, idEmpresaInt);
        if (!string.IsNullOrEmpty(obj.ClassificacaoToxicologica) && obj.ClassificacaoToxicologica.Contains("Categoria"))
        {
            var categoria = obj.ClassificacaoToxicologica.Split('-')[0]
                               .Replace("Categoria", "").Trim();

            obj.ClassificacaoToxicologica = categoria;
        }
            return _mapper.Map<ProdutoViewModel>(obj);
    }

    public async Task AddAsync(ProdutoViewModel obj, string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var mapProduto = _mapper.Map<Domain.Entidades.Cadastros.Produto.Produto>(obj);
        mapProduto.IdEmpresa = idEmpresaInt;
        await _produtoRepository.AddAsync(mapProduto);
    }

    public async Task UpdateAsync(ProdutoViewModel obj)
    {
        var mapProduto = _mapper.Map<Domain.Entidades.Cadastros.Produto.Produto>(obj);
        await _produtoRepository.UpdateAsync(mapProduto);
    }

    public async Task DeleteAsync(int id, string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        await _produtoRepository.DeleteAsync(id, idEmpresaInt);
    }

    public async Task<IEnumerable<string>> GetClasses(string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        return await _produtoRepository.GetClasses(idEmpresaInt);
    }

    public async Task<IEnumerable<string>> GetNomesByIdsAsync(List<int> ids, string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        return await _produtoRepository.GetNomesByIdsAsync(ids, idEmpresaInt);
    }

    public async Task<IEnumerable<ProdutoNomeViewModel>> GetNomes(string classe, string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var produto = await _produtoRepository.GetNomes(classe, idEmpresaInt);
        var viewModelList = produto.Select(p => new ProdutoNomeViewModel
        {
            Id = p.Id,
            Nome = p.Nome
        }).ToList();
        return viewModelList;
    }

    public async Task<ProdutoViewModel> GetByNameAsync(string name, string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var obj = await _produtoRepository.GetByNameAsync(name, idEmpresaInt);
        return _mapper.Map<ProdutoViewModel>(obj);
    }
}
