using Application.DTOs.Cadastros.Produto.Interface;
using Application.DTOs.Cadastros.Produto.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.Produto;

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

    public async Task<IEnumerable<ProdutoViewModel>> GetAllAsync()
    {
        var list = await _produtoRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ProdutoViewModel>>(list);
    }

    public async Task<ProdutoViewModel> GetByIdAsync(int id)
    {
        var obj = await _produtoRepository.GetByIdAsync(id);
        return _mapper.Map<ProdutoViewModel>(obj);
    }

    public async Task AddAsync(ProdutoViewModel obj)
    {
        var mapProduto = _mapper.Map<Domain.Entidades.Cadastros.Produto.Produto>(obj);
        await _produtoRepository.AddAsync(mapProduto);
    }

    public async Task UpdateAsync(ProdutoViewModel obj)
    {
        var mapProduto = _mapper.Map<Domain.Entidades.Cadastros.Produto.Produto>(obj);
        await _produtoRepository.UpdateAsync(mapProduto);
    }

    public async Task DeleteAsync(int id)
    {
        await _produtoRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<string>> GetClasses()
    {
        return await _produtoRepository.GetClasses();
    }

    public async Task<IEnumerable<string>> GetNomes(string classe)
    {
        return await _produtoRepository.GetNomes(classe);
    }
}
