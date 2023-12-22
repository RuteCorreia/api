using Application.DTOs.Cadastros.Controle_De_Frota.Interface;
using Application.DTOs.Cadastros.Controle_De_Frota.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.ControleDeFrota;

namespace Application.Application.Servicos.Cadastros.ControleDeFrota;

public class ControleDeFrotaService : IControleDeFrotaService
{
    private readonly IControleDeFrotaRepository _controleDeFrotaRepository;
    private readonly IMapper _mapper;

    public ControleDeFrotaService(IMapper mapper, IControleDeFrotaRepository controleDeFrotaRepository)
    {
        _controleDeFrotaRepository = controleDeFrotaRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ControleDeFrotaViewModel>> GetAllAsync()
    {
        var list = await _controleDeFrotaRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ControleDeFrotaViewModel>>(list);
    }

    public async Task<ControleDeFrotaViewModel> GetByIdAsync(int id)
    {
        var obj = await _controleDeFrotaRepository.GetByIdAsync(id);
        return _mapper.Map<ControleDeFrotaViewModel>(obj);
    }

    public async Task AddAsync(ControleDeFrotaViewModel obj)
    {
        var mapControleDeFrota = _mapper.Map<Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>(obj);
        await _controleDeFrotaRepository.AddAsync(mapControleDeFrota);
    }

    public async Task UpdateAsync(ControleDeFrotaViewModel obj)
    {
        var mapControleDeFrota = _mapper.Map<Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>(obj);
        await _controleDeFrotaRepository.UpdateAsync(mapControleDeFrota);
    }

    public async Task DeleteAsync(int id)
    {
        await _controleDeFrotaRepository.DeleteAsync(id);
    }
}
