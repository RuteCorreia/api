using Application.DTOs.Cadastros.ContratoPrestacaoServico.Interface;
using Application.DTOs.Cadastros.ContratoPrestacaoServico.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.ContratoPrestacaoServico;
using Helpers;

namespace Application.Application.Servicos.Cadastros.ContratoPrestacaoServico;

public class ContratoPrestacaoServicoService : IContratoPrestacaoServicoService
{
    private readonly IContratoPrestacaoServicoRepository _contratoPrestacaoServicoRepository;
    private readonly IMapper _mapper;

    public ContratoPrestacaoServicoService(
        IMapper mapper,
        IContratoPrestacaoServicoRepository contratoPrestacaoServicoRepository
    )
    {
        _mapper = mapper;
        _contratoPrestacaoServicoRepository = contratoPrestacaoServicoRepository;
    }

    public async Task<int> AddAsync(ContratoPrestacaoServicoViewModel obj, string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var mapObj = _mapper.Map<Domain.Entidades.Cadastros.ContratoPrestacaoServico.ContratoPrestacaoServico>(obj);
        mapObj.IdEmpresa = idEmpresaInt == 0 ? null : idEmpresaInt;

        if (obj.Id > 0)
            await _contratoPrestacaoServicoRepository.UpdateAsync(mapObj);

        var contratoPrestacaoServico = _contratoPrestacaoServicoRepository.AddAsync(mapObj);
        return contratoPrestacaoServico.Result;
    }

    public async Task DeleteAsync(int id, string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        await _contratoPrestacaoServicoRepository.DeleteAsync(id, idEmpresaInt);
    }

    public async Task<IEnumerable<ContratoPrestacaoServicoViewModel>> GetAllAsync(string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var list = await _contratoPrestacaoServicoRepository.GetAllAsync(idEmpresaInt);
        return _mapper.Map<IEnumerable<ContratoPrestacaoServicoViewModel>>(list);
    }

    public async Task<ContratoPrestacaoServicoViewModel> GetByIdAsync(int id, string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var obj = await _contratoPrestacaoServicoRepository.GetByIdAsync(id, idEmpresaInt);
        return _mapper.Map<ContratoPrestacaoServicoViewModel>(obj);
    }

    public async Task UpdateAsync(ContratoPrestacaoServicoViewModel obj)
    {
        var mapObj = _mapper.Map<Domain.Entidades.Cadastros.ContratoPrestacaoServico.ContratoPrestacaoServico>(obj);
        await _contratoPrestacaoServicoRepository.UpdateAsync(mapObj);
    }
}
