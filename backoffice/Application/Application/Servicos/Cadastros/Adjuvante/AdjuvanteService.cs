using Application.DTOs.Cadastros.Adjuvante.Interface;
using Application.DTOs.Cadastros.Adjuvante.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.Adjuvante;

namespace Application.Application.Servicos.Cadastros.Adjuvante;
public class AdjuvanteService : IAdjuvanteService
{
    private readonly IAdjuvanteRepository _adjuvanteRepository;
    private readonly IMapper _mapper;

    public AdjuvanteService(IMapper mapper, IAdjuvanteRepository adjuvanteRepository)
    {
        _adjuvanteRepository = adjuvanteRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AdjuvanteViewModel>> GetAllAsync()
    {
        var list = await _adjuvanteRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<AdjuvanteViewModel>>(list);
    }

    public async Task<AdjuvanteViewModel> GetByIdAsync(int id)
    {
        var obj = await _adjuvanteRepository.GetByIdAsync(id);
        return _mapper.Map<AdjuvanteViewModel>(obj);
    }

    public async Task AddAsync(AdjuvanteViewModel obj)
    {
        var mapAdjuvante = _mapper.Map<Domain.Entidades.Cadastros.Adjuvante.Adjuvante>(obj);
        await _adjuvanteRepository.AddAsync(mapAdjuvante);
    }

    public async Task UpdateAsync(AdjuvanteViewModel obj)
    {
        var mapAdjuvante = _mapper.Map<Domain.Entidades.Cadastros.Adjuvante.Adjuvante>(obj);
        await _adjuvanteRepository.UpdateAsync(mapAdjuvante);
    }

    public async Task DeleteAsync(int id)
    {
        await _adjuvanteRepository.DeleteAsync(id);
    }
}
