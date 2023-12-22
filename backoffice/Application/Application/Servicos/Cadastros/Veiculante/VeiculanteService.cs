using Application.DTOs.Cadastros.Veiculante.Interface;
using Application.DTOs.Cadastros.Veiculante.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.Veiculante;

namespace Application.Application.Servicos.Cadastros.Veiculante;

public class VeiculanteService : IVeiculanteService
{
    private readonly IVeiculanteRepository _veiculanteRepository;
    private readonly IMapper _mapper;

    public VeiculanteService(IMapper mapper, IVeiculanteRepository veiculanteRepository)
    {
        _veiculanteRepository = veiculanteRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<VeiculanteViewModel>> GetAllAsync()
    {
        var list = await _veiculanteRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<VeiculanteViewModel>>(list);
    }

    public async Task<VeiculanteViewModel> GetByIdAsync(int id)
    {
        var obj = await _veiculanteRepository.GetByIdAsync(id);
        return _mapper.Map<VeiculanteViewModel>(obj);
    }

    public async Task AddAsync(VeiculanteViewModel obj)
    {
        var mapVeiculante = _mapper.Map<Domain.Entidades.Cadastros.Veiculante.Veiculante>(obj);
        await _veiculanteRepository.AddAsync(mapVeiculante);
    }

    public async Task UpdateAsync(VeiculanteViewModel obj)
    {
        var mapVeiculante = _mapper.Map<Domain.Entidades.Cadastros.Veiculante.Veiculante>(obj);
        await _veiculanteRepository.UpdateAsync(mapVeiculante);
    }

    public async Task DeleteAsync(int id)
    {
        await _veiculanteRepository.DeleteAsync(id);
    }
}
