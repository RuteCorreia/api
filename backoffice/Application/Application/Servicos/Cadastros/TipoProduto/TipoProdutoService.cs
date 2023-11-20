using Application.DTOs.Cadastros.Tipo_Produto.Interface;
using Application.DTOs.Cadastros.Tipo_Produto.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.TipoProduto;

namespace Application.Application.Servicos.Cadastros.TipoProduto;

public class TipoProdutoService : ITipoProdutoService
{
    private readonly ITipoProdutoRepository _tipoProdutoRepository;
    private readonly IMapper _mapper;

    public TipoProdutoService(IMapper mapper, ITipoProdutoRepository tipoProdutoRepository)
    {
        _tipoProdutoRepository = tipoProdutoRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TipoProdutoViewModel>> GetAllAsync()
    {
        var list = await _tipoProdutoRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<TipoProdutoViewModel>>(list);
    }

    public async Task<TipoProdutoViewModel> GetByIdAsync(int id)
    {
        var obj = await _tipoProdutoRepository.GetByIdAsync(id);
        return _mapper.Map<TipoProdutoViewModel>(obj);
    }

    public async Task AddAsync(TipoProdutoViewModel obj)
    {
        var mapTipoProduto = _mapper.Map<Domain.Entidades.Cadastros.Tipo_Produto.TipoProduto>(obj);
        await _tipoProdutoRepository.AddAsync(mapTipoProduto);
    }

    public async Task UpdateAsync(TipoProdutoViewModel obj)
    {
        var mapTipoProduto = _mapper.Map<Domain.Entidades.Cadastros.Tipo_Produto.TipoProduto>(obj);
        await _tipoProdutoRepository.UpdateAsync(mapTipoProduto);
    }

    public async Task DeleteAsync(int id)
    {
        await _tipoProdutoRepository.DeleteAsync(id);
    }
}
