using Application.DTOs.Cadastros.Estados.Interface;
using Application.DTOs.Cadastros.Estados.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.Estados;

namespace Application.Application.Servicos.Cadastros.Estados;

public class EstadosService : IEstadosService
{
    private readonly IEstadosRepository _estadosRepository;
    private readonly IMapper _mapper;

    public EstadosService(IMapper mapper, IEstadosRepository estadosRepository)
    {
        _estadosRepository = estadosRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EstadosViewModel>> GetAllAsync()
    {
        var list = await _estadosRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<EstadosViewModel>>(list);
    }

    public async Task<EstadosViewModel> GetByIdAsync(int id)
    {
        var obj = await _estadosRepository.GetByIdAsync(id);
        return _mapper.Map<EstadosViewModel>(obj);
    }

    public async Task AddAsync(EstadosViewModel obj)
    {
        var mapEstados = _mapper.Map<Domain.Entidades.Cadastros.Estados.Estados>(obj);
        await _estadosRepository.AddAsync(mapEstados);
    }

    public async Task UpdateAsync(EstadosViewModel obj)
    {
        var mapEstados = _mapper.Map<Domain.Entidades.Cadastros.Estados.Estados>(obj);
        await _estadosRepository.UpdateAsync(mapEstados);
    }

    public async Task DeleteAsync(int id)
    {
        await _estadosRepository.DeleteAsync(id);
    }
}
