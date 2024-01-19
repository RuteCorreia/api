using Application.DTOs.Cadastros.Bula.Interface;
using Application.DTOs.Cadastros.Bula.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.Bula;

namespace Application.Application.Servicos.Cadastros.Bula;

public class BulaService : IBulaService
{
    private readonly IBulaRepository _bulaRepository;
    private readonly IMapper _mapper;

    public BulaService(IMapper mapper, IBulaRepository bulaRepository)
    {
        _bulaRepository = bulaRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BulaViewModel>> GetAllAsync()
    {
        var list = await _bulaRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<BulaViewModel>>(list);
    }

    public async Task<BulaViewModel> GetByIdAsync(int id)
    {
        var obj = await _bulaRepository.GetByIdAsync(id);
        return _mapper.Map<BulaViewModel>(obj);
    }

    public async Task AddAsync(BulaViewModel obj)
    {
        var mapBula = _mapper.Map<Domain.Entidades.Cadastros.Empresa.Bula>(obj);
        await _bulaRepository.AddAsync(mapBula);
    }

    public async Task UpdateAsync(BulaViewModel obj)
    {
        var mapBula = _mapper.Map<Domain.Entidades.Cadastros.Empresa.Bula>(obj);
        await _bulaRepository.UpdateAsync(mapBula);
    }

    public async Task DeleteAsync(int id)
    {
        await _bulaRepository.DeleteAsync(id);
    }
}
