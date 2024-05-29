using Application.DTOs.Cadastros.Cidades.Interface;
using Application.DTOs.Cadastros.Cidades.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.Cidades;

namespace Application.Application.Servicos.Cadastros.Cidades;

public class CidadesService : ICidadeService
{
    private readonly ICidadeRepository _cidadeRepository;
    private readonly IMapper _mapper;

    public CidadesService(IMapper mapper, ICidadeRepository cidadeRepository)
    {
        _cidadeRepository = cidadeRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CidadeViewModel>> GetAllAsync()
    {
        var list = await _cidadeRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CidadeViewModel>>(list);
    }

    public async Task<CidadeViewModel> GetByIdAsync(int id)
    {
        var obj = await _cidadeRepository.GetByIdAsync(id);
        return _mapper.Map<CidadeViewModel>(obj);
    }

    public async Task AddAsync(CidadeViewModel obj)
    {
        var mapCidade = _mapper.Map<Domain.Entidades.Cadastros.Cidades.Cidades>(obj);
        await _cidadeRepository.AddAsync(mapCidade);
    }

    public async Task UpdateAsync(CidadeViewModel obj)
    {
        var mapCidade = _mapper.Map<Domain.Entidades.Cadastros.Cidades.Cidades>(obj);
        await _cidadeRepository.UpdateAsync(mapCidade);
    }

    public async Task DeleteAsync(int id)
    {
        await _cidadeRepository.DeleteAsync(id);
    }
}