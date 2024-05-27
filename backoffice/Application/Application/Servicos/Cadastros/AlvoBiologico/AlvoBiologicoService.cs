using Application.DTOs.Cadastros.AlvoBiologico.Interface;
using Application.DTOs.Cadastros.AlvoBiologico.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.AlvoBiologico;

namespace Application.Application.Servicos.Cadastros.AlvoBiologico;

public class AlvoBiologicoService : IAlvoBiologicoService
{
    private readonly IAlvoBiologicoRepository _alvoBiologicoRepository;
    private readonly IMapper _mapper;

    public AlvoBiologicoService(IMapper mapper, IAlvoBiologicoRepository alvoBiologicoRepository)
    {
        _alvoBiologicoRepository = alvoBiologicoRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AlvoBiologicoViewModel>> GetAllAsync()
    {
        var list = await _alvoBiologicoRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<AlvoBiologicoViewModel>>(list);
    }

    public async Task<AlvoBiologicoViewModel> GetByIdAsync(int id)
    {
        var obj = await _alvoBiologicoRepository.GetByIdAsync(id);
        return _mapper.Map<AlvoBiologicoViewModel>(obj);
    }

    public async Task AddAsync(AlvoBiologicoViewModel obj)
    {
        var mapAlvoBiologico = _mapper.Map<Domain.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico>(obj);
        await _alvoBiologicoRepository.AddAsync(mapAlvoBiologico);
    }

    public async Task UpdateAsync(AlvoBiologicoViewModel obj)
    {
        var mapAlvoBiologico = _mapper.Map<Domain.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico>(obj);
        await _alvoBiologicoRepository.UpdateAsync(mapAlvoBiologico);
    }

    public async Task DeleteAsync(int id)
    {
        await _alvoBiologicoRepository.DeleteAsync(id);
    }

    public async Task<AlvoBiologicoViewModel> GetByName(string name)
    {
        var obj = await _alvoBiologicoRepository.GetByNameAsync(name);
        return _mapper.Map<AlvoBiologicoViewModel>(obj);
    }
}
