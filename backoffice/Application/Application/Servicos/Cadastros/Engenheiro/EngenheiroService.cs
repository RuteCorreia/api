using Application.DTOs.Cadastros.Engenheiro.Interface;
using Application.DTOs.Cadastros.Engenheiro.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.Engenheiro;

namespace Application.Application.Servicos.Cadastros.Engenheiro;

public class EngenheiroService : IEngenheiroService
{
    private readonly IEngenheiroRepository _engenheiroRepository;
    private readonly IMapper _mapper;

    public EngenheiroService(IMapper mapper, IEngenheiroRepository engenheiroRepository)
    {
        _engenheiroRepository = engenheiroRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EngenheiroViewModel>> GetAllAsync()
    {
        var list = await _engenheiroRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<EngenheiroViewModel>>(list);
    }

    public async Task<EngenheiroViewModel> GetByIdAsync(int id)
    {
        var obj = await _engenheiroRepository.GetByIdAsync(id);
        return _mapper.Map<EngenheiroViewModel>(obj);
    }

    public async Task AddAsync(EngenheiroViewModel obj)
    {
        var mapEngenheiro = _mapper.Map<Domain.Entidades.Cadastros.Engenheiro.Engenheiro>(obj);
        await _engenheiroRepository.AddAsync(mapEngenheiro);
    }

    public async Task UpdateAsync(EngenheiroViewModel obj)
    {
        var mapEngenheiro = _mapper.Map<Domain.Entidades.Cadastros.Engenheiro.Engenheiro>(obj);
        await _engenheiroRepository.UpdateAsync(mapEngenheiro);
    }

    public async Task DeleteAsync(int id)
    {
        await _engenheiroRepository.DeleteAsync(id);
    }
}
