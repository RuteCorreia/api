using Application.DTOs.Cadastros.Engenheiro.Interface;
using Application.DTOs.Cadastros.Engenheiro.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.Engenheiro;
using Helpers;

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

    public async Task<IEnumerable<EngenheiroViewModel>> GetAllAsync(string? idEmpresa)
    {
        var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
        var usuarioCredencialList = await _engenheiroRepository.GetAllAsync(idEmpresaInt);
        var usuarioList = usuarioCredencialList
            .Select(u => u.Usuario)
            .ToList();
        var mappedList = _mapper.Map<IEnumerable<EngenheiroViewModel>>(usuarioList);
        var usuarioDict = usuarioList.ToDictionary(u => u.Id, u => u);
        var credencialDict = usuarioCredencialList.ToDictionary(uc => uc.IdUsuario, uc => uc.Credencial);
        
        var returnList = mappedList.Select(x => new EngenheiroViewModel
        {
            Id = x.Id,
            Nome = x.Nome,
            Email = x.Email,
            Telefone = x.Telefone,
            Assinatura = usuarioDict.TryGetValue(Guid.Parse(x.Id), out var usuario) ?
                Convert.ToBase64String(usuario?.Assinatura ?? [])
                : null,
            CREA = credencialDict.TryGetValue(Guid.Parse(x.Id), out var credencial) ?
                credencial
                : null
        });
        return returnList;
    }

    public async Task<EngenheiroViewModel?> GetByIdAsync(string id, string? idEmpresa)
    {
        var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
        var usuarioCredencialObj = await _engenheiroRepository.GetByIdAsync(id, idEmpresaInt);
        var usuarioObj = usuarioCredencialObj?.Usuario ?? null; 
        var mappedObj = usuarioObj is not null ? _mapper.Map<EngenheiroViewModel>(usuarioObj) : null;
        if(mappedObj is not null)
        {
            mappedObj.Assinatura = Convert.ToBase64String(usuarioObj?.Assinatura ?? []);
            mappedObj.CREA = usuarioCredencialObj?.Credencial;
        }
        
        return mappedObj;
    }
}
