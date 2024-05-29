using Application.DTOs.Cadastros.Piloto.Interface;
using Application.DTOs.Cadastros.Piloto.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.Piloto;
using Helpers;

namespace Application.Application.Servicos.Cadastros.Piloto;

public class PilotoService : IPilotoService
{
    private readonly IPilotoRepository _pilotoRepository;
    private readonly IMapper _mapper;

    public PilotoService(IMapper mapper, IPilotoRepository pilotoRepository)
    {
        _pilotoRepository = pilotoRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PilotoViewModel>> GetAllAsync(string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var usuarioCredencialList = await _pilotoRepository.GetAllAsync(idEmpresaInt);
        var usuarioList = usuarioCredencialList
            .Select(u => u.Usuario)
            .ToList();
        var mappedList = _mapper.Map<IEnumerable<PilotoViewModel>>(usuarioList);
        var usuarioDict = usuarioList.ToDictionary(u => u.Id, u => u);
        var credencialDict = usuarioCredencialList.ToDictionary(uc => uc.IdUsuario, uc => uc.Credencial);

        var returnList = mappedList.Select(x => new PilotoViewModel
        {
            Id = x.Id,
            Nome = x.Nome,
            Email = x.Email,
            Telefone = x.Telefone,
            Assinatura = usuarioDict.TryGetValue(Guid.Parse(x.Id), out var usuario) ?
               Convert.ToBase64String(usuario?.Assinatura ?? [])
               : null,
            CDAC = credencialDict.TryGetValue(Guid.Parse(x.Id), out var credencial) ?
               credencial
               : null
        });
        return returnList;
    }

    public async Task<PilotoViewModel?> GetByIdAsync(string id, string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var usuarioCredencialObj = await _pilotoRepository.GetByIdAsync(id, idEmpresaInt);
        var usuarioObj = usuarioCredencialObj?.Usuario ?? null;
        var mappedObj = usuarioObj is not null ? _mapper.Map<PilotoViewModel>(usuarioObj) : null;
        if (mappedObj is not null)
        {
            mappedObj.Assinatura = Convert.ToBase64String(usuarioObj?.Assinatura ?? []);
            mappedObj.CDAC = usuarioCredencialObj?.Credencial;
        }

        return mappedObj;
    }
}
