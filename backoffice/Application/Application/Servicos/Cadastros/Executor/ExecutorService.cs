using Application.DTOs.Cadastros.Executor.Interface;
using Application.DTOs.Cadastros.Executor.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.Executor;
using Helpers;

namespace Application.Application.Servicos.Cadastros.Executor;

public class ExecutorService : IExecutorService
{
    private readonly IExecutorRepository _executorRepository;
    private readonly IMapper _mapper;

    public ExecutorService(IMapper mapper, IExecutorRepository executorRepository)
    {
        _executorRepository = executorRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ExecutorViewModel>> GetAllAsync(string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var usuarioCredencialList = await _executorRepository.GetAllAsync(idEmpresaInt);
        var usuarioList = usuarioCredencialList
            .Select(u => u.Usuario)
            .ToList();
        var mappedList = _mapper.Map<IEnumerable<ExecutorViewModel>>(usuarioList);
        var usuarioDict = usuarioList.ToDictionary(u => u.Id, u => u);
        var credencialDict = usuarioCredencialList.ToDictionary(uc => uc.IdUsuario, uc => uc.Credencial);

        var returnList = mappedList.Select(x => new ExecutorViewModel
        {
            Id = x.Id,
            Nome = x.Nome,
            Email = x.Email,
            Telefone = x.Telefone,
            Assinatura = usuarioDict.TryGetValue(Guid.Parse(x.Id), out var usuario) ?
               Convert.ToBase64String(usuario?.Assinatura ?? [])
               : null,
            CFTA = credencialDict.TryGetValue(Guid.Parse(x.Id), out var credencial) ?
               credencial
               : null
        });
        return returnList;
    }

    public async Task<ExecutorViewModel?> GetByIdAsync(string id, string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var usuarioCredencialObj = await _executorRepository.GetByIdAsync(id, idEmpresaInt);
        var usuarioObj = usuarioCredencialObj?.Usuario ?? null;
        var mappedObj = usuarioObj is not null ? _mapper.Map<ExecutorViewModel>(usuarioObj) : null;
        if (mappedObj is not null)
        {
            mappedObj.Assinatura = Convert.ToBase64String(usuarioObj?.Assinatura ?? []);
            mappedObj.CFTA = usuarioCredencialObj?.Credencial;
        }

        return mappedObj;
    }
}