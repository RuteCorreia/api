using Application.DTOs.Cadastros.Piloto.Interface;
using Application.DTOs.Cadastros.Piloto.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.Piloto;

namespace Application.Application.Servicos.Cadastros.Piloto;

public class PilotoService : IPilotoService
{
    private readonly IPilotoRepository _pilotoRepository;
    private readonly IMapper _mapper;

    public PilotoService(IMapper mapper, IPilotoRepository pilotoRepository)
    {
        _pilotoRepository = pilotoRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PilotoViewModel>> GetAllAsync()
    {
        var list = await _pilotoRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<PilotoViewModel>>(list);
    }

    public async Task<PilotoViewModel> GetByIdAsync(int id)
    {
        var obj = await _pilotoRepository.GetByIdAsync(id);
        return _mapper.Map<PilotoViewModel>(obj);
    }

    public async Task AddAsync(PilotoViewModel obj)
    {
        var mapPiloto = _mapper.Map<Domain.Entidades.Cadastros.Piloto.Piloto>(obj);
        await _pilotoRepository.AddAsync(mapPiloto);
    }

    public async Task UpdateAsync(PilotoViewModel obj)
    {
        var mapPiloto = _mapper.Map<Domain.Entidades.Cadastros.Piloto.Piloto>(obj);
        await _pilotoRepository.UpdateAsync(mapPiloto);
    }

    public async Task DeleteAsync(int id)
    {
        await _pilotoRepository.DeleteAsync(id);
    }
}
