using Application.DTOs.Cadastros.AplicacaoRelatorio.Interface;
using Application.DTOs.Cadastros.AplicacaoRelatorio.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.AplicacaoRelatorio;
using Domain.Interfaces.Cadastros.AplicacaoRelatorioItem;

namespace Application.Application.Servicos.Cadastros.AplicacaoRelatorio;

public class AplicacaoRelatorioService : IAplicacaoRelatorioService
{
    private readonly IAplicacaoRelatorioItemRepository _aplicacaoRelatorioItemRepository;
    private readonly IAplicacaoRelatorioRepository _aplicacaoRelatorioRepository;
    private readonly IMapper _mapper;

    public AplicacaoRelatorioService(
        IAplicacaoRelatorioRepository aplicacaoRelatorioRepository,
        IAplicacaoRelatorioItemRepository aplicacaoRelatorioItemRepository,
        IMapper mapper 
        )
    {
        _aplicacaoRelatorioItemRepository = aplicacaoRelatorioItemRepository;
        _aplicacaoRelatorioRepository = aplicacaoRelatorioRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AplicacaoRelatorioViewModel>> GetAllAsync()
    {
        var list = await _aplicacaoRelatorioRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<AplicacaoRelatorioViewModel>>(list);
    }

    public async Task<AplicacaoRelatorioViewModel> GetByIdAsync(int id)
    {
        var obj = await _aplicacaoRelatorioRepository.GetByIdAsync(id);
        return _mapper.Map<AplicacaoRelatorioViewModel>(obj);
    }

    public async Task<int> AddAsync(AplicacaoRelatorioViewModel obj)
    {
        var mapAplicacaoRelatorio = _mapper.Map<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorio>(obj);
        await _aplicacaoRelatorioRepository.AddAsync(mapAplicacaoRelatorio);

        if (obj.Aplicacoes != null && obj.Aplicacoes.Any())
        {
            foreach (var aplicacaoItem in obj.Aplicacoes)
            {
                var mapAplicacaoRelatorioItem = _mapper.Map<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem>(aplicacaoItem);
                mapAplicacaoRelatorioItem.IdAplicacaoRelatorio = mapAplicacaoRelatorio.Id; // Atribua o Id da entidade principal

                await _aplicacaoRelatorioItemRepository.AddAsync(mapAplicacaoRelatorioItem);
            }
        }
        
        return mapAplicacaoRelatorio.Id;
        
    }

    public async Task UpdateAsync(AplicacaoRelatorioViewModel obj)
    {
        var mapAplicacaoRelatorio = _mapper.Map<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorio>(obj);
        await _aplicacaoRelatorioRepository.UpdateAsync(mapAplicacaoRelatorio);
    }

    public async Task DeleteAsync(int id)
    {
        await _aplicacaoRelatorioRepository.DeleteAsync(id);
    }
}
