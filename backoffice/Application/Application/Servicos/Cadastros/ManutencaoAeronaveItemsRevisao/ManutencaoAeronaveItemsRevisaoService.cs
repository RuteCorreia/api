using Application.DTOs.Cadastros.ManutencaoAeronave.ViewModel;
using Application.DTOs.Cadastros.ManutencaoAeronaveItemsRevisao.Interface;
using Application.DTOs.Cadastros.ManutencaoAeronaveItemsRevisao.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.ManutencaoAeronave;
using Domain.Interfaces.Cadastros.ManutencaoAeronaveItemsRevisao;
using Infra.Repositorio.Cadastros.ManutencaoAeronave;

namespace Application.Application.Servicos.Cadastros.ManutencaoAeronaveItemsRevisao;

public class ManutencaoAeronaveItemsRevisaoService : IManutencaoAeronaveItemsRevisaoService
{
    private readonly IManutencaoAeronaveItemsRevisaoRepository _manutencaoItemsRevisaoRepository;
    private readonly IMapper _mapper;

    public ManutencaoAeronaveItemsRevisaoService(
    IMapper mapper,
    IManutencaoAeronaveItemsRevisaoRepository manutencaoItemsRevisaoRepository
)
    {
        _manutencaoItemsRevisaoRepository = manutencaoItemsRevisaoRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ManutencaoAeronaveItemsRevisaoViewModel>> GetByIdAeronaveAsync(int id)
    {
        var list = await _manutencaoItemsRevisaoRepository.GetAllByManutencaoAeronaveIdAsync(id);
        return _mapper.Map<IEnumerable<ManutencaoAeronaveItemsRevisaoViewModel>>(list);
    }
}
