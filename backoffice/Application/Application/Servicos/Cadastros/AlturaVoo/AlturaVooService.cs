using Application.DTOs.Cadastros.AlturaVoo.Interface;
using Application.DTOs.Cadastros.AlturaVoo.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.AlturaVoo;

namespace Application.Application.Servicos.Cadastros.AlturaVoo;

public class AlturaVooService : IAlturaVooService
{
    private readonly IAlturaVooRepository _alturaVooRepository;
    private readonly IMapper _mapper;

    public AlturaVooService(IMapper mapper, IAlturaVooRepository alturaVooRepository)
    {
        _alturaVooRepository = alturaVooRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AlturaVooViewModel>> GetAllAsync()
    {
        var list = await _alturaVooRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<AlturaVooViewModel>>(list);
    }

    public async Task<AlturaVooViewModel> GetByIdAsync(int id)
    {
        var obj = await _alturaVooRepository.GetByIdAsync(id);
        return _mapper.Map<AlturaVooViewModel>(obj);
    }

    public async Task AddAsync(AlturaVooViewModel obj)
    {
        var mapAlturaVoo = _mapper.Map<Domain.Entidades.Cadastros.Altura_Voo.AlturaVoo>(obj);
        await _alturaVooRepository.AddAsync(mapAlturaVoo);
    }

    public async Task UpdateAsync(AlturaVooViewModel obj)
    {
        var mapAlturaVoo = _mapper.Map<Domain.Entidades.Cadastros.Altura_Voo.AlturaVoo>(obj);
        await _alturaVooRepository.UpdateAsync(mapAlturaVoo);
    }

    public async Task DeleteAsync(int id)
    {
        await _alturaVooRepository.DeleteAsync(id);
    }
}
