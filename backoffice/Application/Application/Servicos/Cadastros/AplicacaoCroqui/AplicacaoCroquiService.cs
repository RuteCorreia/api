using Application.DTOs.Cadastros.AplicacaoCroqui.Interface;
using Application.DTOs.Cadastros.AplicacaoCroqui.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.AplicacaoCroqui;

namespace Application.Application.Servicos.Cadastros.AplicacaoCroqui;

public class AplicacaoCroquiService : IAplicacaoCroquiService
{
    private readonly IAplicacaoCroquiRepository _aplicacaoCroquiRepository;
    private readonly IMapper _mapper;

    public AplicacaoCroquiService(IMapper mapper, IAplicacaoCroquiRepository aplicacaoCroquiRepository)
    {
        _aplicacaoCroquiRepository = aplicacaoCroquiRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AplicacaoCroquiViewModel>> GetAllAsync()
    {
        var list = await _aplicacaoCroquiRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<AplicacaoCroquiViewModel>>(list);
    }

    public async Task<AplicacaoCroquiViewModel> GetByIdAsync(int id)
    {
        var obj = await _aplicacaoCroquiRepository.GetByIdAsync(id);
        return _mapper.Map<AplicacaoCroquiViewModel>(obj);
    }

    public async Task AddAsync(AplicacaoCroquiViewModel obj)
    {
        var mapAplicacaoCroqui = _mapper.Map<Domain.Entidades.Cadastros.Aplicacao.AplicacaoCroqui>(obj);
        await _aplicacaoCroquiRepository.AddAsync(mapAplicacaoCroqui);
    }

    public async Task UpdateAsync(AplicacaoCroquiViewModel obj)
    {
        var mapAplicacaoCroqui = _mapper.Map<Domain.Entidades.Cadastros.Aplicacao.AplicacaoCroqui>(obj);
        await _aplicacaoCroquiRepository.UpdateAsync(mapAplicacaoCroqui);
    }

    public async Task DeleteAsync(int id)
    {
        await _aplicacaoCroquiRepository.DeleteAsync(id);
    }
}
