using Application.DTOs.Cadastros.AplicacaoRelatorioItem.Interface;
using Application.DTOs.Cadastros.AplicacaoRelatorioItem.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.AplicacaoRelatorioItem;

namespace Application.Application.Servicos.Cadastros.AplicacaoRelatorioItem;

public class AplicacaoRelatorioItemService : IAplicacaoRelatorioItemService
{
    private readonly IAplicacaoRelatorioItemRepository _aplicacaoRelatorioItemRepository;
    private readonly IMapper _mapper;

    public AplicacaoRelatorioItemService(IMapper mapper, IAplicacaoRelatorioItemRepository aplicacaoRelatorioItemRepository)
    {
        _aplicacaoRelatorioItemRepository = aplicacaoRelatorioItemRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RelatorioItemViewModel>> GetAllAsync(int idRelatorioAplicacao)
    {
        var list = await _aplicacaoRelatorioItemRepository.GetAllByAplicacaoRelatorioIdAsync(idRelatorioAplicacao);
        return _mapper.Map<IEnumerable<RelatorioItemViewModel>>(list);
    }

    public async Task<IEnumerable<RelatorioItemViewModel>> GetAllByAplicacaoRelatorioIdAsync(int aplicacaoRelatorioId)
    {
        var list = await _aplicacaoRelatorioItemRepository.GetAllByAplicacaoRelatorioIdAsync(aplicacaoRelatorioId);
        return _mapper.Map<IEnumerable<RelatorioItemViewModel>>(list);
    }


    public async Task<AplicacaoRelatorioItemViewModel> GetByIdAsync(int id)
    {
        var obj = await _aplicacaoRelatorioItemRepository.GetByIdAsync(id);
        return _mapper.Map<AplicacaoRelatorioItemViewModel>(obj);
    }

    public async Task AddAsync(AplicacaoRelatorioItemViewModel obj)
    {
        var mapAplicacaoRelatorioItem = _mapper.Map<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem>(obj);
        await _aplicacaoRelatorioItemRepository.AddAsync(mapAplicacaoRelatorioItem);
    }

    //public async Task UpdateAsync(List<AplicacaoRelatorioItemViewModel> objs)
    //{
    //    foreach (var obj in objs)
    //    {
    //        var mapAplicacaoRelatorioItem = _mapper.Map<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem>(obj);
    //        await _aplicacaoRelatorioItemRepository.UpdateAsync(mapAplicacaoRelatorioItem);
    //    }
    //}

    public async Task DeleteAsync(int id)
    {
        await _aplicacaoRelatorioItemRepository.DeleteAsync(id);
    }
}
