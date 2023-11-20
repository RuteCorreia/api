using Application.DTOs.Cadastros.Equipamento.Interface;
using Application.DTOs.Cadastros.Equipamento.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.Equipamento;

namespace Application.Application.Servicos.Cadastros.Equipamento;

public class EquipamentoService : IEquipamentoService
{
    private readonly IEquipamentoRepository _equipamentoRepository;
    private readonly IMapper _mapper;

    public EquipamentoService(IMapper mapper, IEquipamentoRepository equipamentoRepository)
    {
        _equipamentoRepository = equipamentoRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EquipamentoViewModel>> GetAllAsync()
    {
        var list = await _equipamentoRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<EquipamentoViewModel>>(list);
    }

    public async Task<EquipamentoViewModel> GetByIdAsync(int id)
    {
        var obj = await _equipamentoRepository.GetByIdAsync(id);
        return _mapper.Map<EquipamentoViewModel>(obj);
    }

    public async Task AddAsync(EquipamentoViewModel obj)
    {
        var mapEquipamento = _mapper.Map<Domain.Entidades.Cadastros.Equipamento.Equipamento>(obj);
        await _equipamentoRepository.AddAsync(mapEquipamento);
    }

    public async Task UpdateAsync(EquipamentoViewModel obj)
    {
        var mapEquipamento = _mapper.Map<Domain.Entidades.Cadastros.Equipamento.Equipamento>(obj);
        await _equipamentoRepository.UpdateAsync(mapEquipamento);
    }

    public async Task DeleteAsync(int id)
    {
        await _equipamentoRepository.DeleteAsync(id);
    }
}
