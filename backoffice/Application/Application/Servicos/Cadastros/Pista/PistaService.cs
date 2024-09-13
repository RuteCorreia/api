using Application.DTOs.Cadastros.Pistas.Interface;
using Application.DTOs.Cadastros.Pistas.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.Pista;
using System.Text.RegularExpressions;

namespace Application.Application.Servicos.Cadastros.Pista;

public class PistaService : IPistaService
{
    private readonly IPistaRepository _pistaRepository;
    private readonly IMapper _mapper;

    public PistaService(IMapper mapper, IPistaRepository pistaRepository)
    {
        _pistaRepository = pistaRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PistaViewModel>> GetAllAsync()
    {
        var list = await _pistaRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<PistaViewModel>>(list);
    }

    public async Task<PistaViewModel> GetByIdAsync(int id)
    {
        var obj = await _pistaRepository.GetByIdAsync(id);
        return _mapper.Map<PistaViewModel>(obj);
    }

    public async Task<IEnumerable<PistaViewModel>> GetByNameAsync(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("O nome não pode ser nulo ou vazio.", nameof(name));
        }
        var pistas = await _pistaRepository.GetByNameAsync(name);

        return pistas.Select(p => _mapper.Map<PistaViewModel>(p));
    }

    public async Task<int> AddAsync(PistaViewModel obj)
    {
        var mapPista = _mapper.Map<Domain.Entidades.Cadastros.Pistas.Pista>(obj);
        return await _pistaRepository.AddAsync(mapPista);
    }

    public async Task UpdateAsync(PistaViewModel obj)
    {
        var mapPista = _mapper.Map<Domain.Entidades.Cadastros.Pistas.Pista>(obj);
        await _pistaRepository.UpdateAsync(mapPista);
    }

    public async Task DeleteAsync(int id)
    {
        await _pistaRepository.DeleteAsync(id);
    }
}
