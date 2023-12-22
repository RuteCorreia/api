using Application.DTOs.Cadastros.Frota.Interface;
using Application.DTOs.Cadastros.Frota.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.Frota;

namespace Application.Application.Servicos.Cadastros.Frota;

public class FrotaService : IFrotaService
{
    private readonly IFrotaRepository _frotaRepository;
    private readonly IMapper _mapper;

    public FrotaService(IMapper mapper, IFrotaRepository frotaRepository)
    {
        _frotaRepository = frotaRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<FrotaViewModel>> GetAllAsync()
    {
        var list = await _frotaRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<FrotaViewModel>>(list);
    }

    public async Task<FrotaViewModel> GetByIdAsync(int id)
    {
        var obj = await _frotaRepository.GetByIdAsync(id);
        return _mapper.Map<FrotaViewModel>(obj);
    }

    public async Task AddAsync(FrotaViewModel obj)
    {
        var mapFrota = _mapper.Map<Domain.Entidades.Cadastros.Frota.Frota>(obj);
        await _frotaRepository.AddAsync(mapFrota);
    }

    public async Task UpdateAsync(FrotaViewModel obj)
    {
        var mapFrota = _mapper.Map<Domain.Entidades.Cadastros.Frota.Frota>(obj);
        await _frotaRepository.UpdateAsync(mapFrota);
    }

    public async Task DeleteAsync(int id)
    {
        await _frotaRepository.DeleteAsync(id);
    }
}
