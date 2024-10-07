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
        var pilotoList = await _executorRepository.GetAllAsync(idEmpresaInt);
        var viewModel = _mapper.Map<IEnumerable<ExecutorViewModel>>(pilotoList);
        return viewModel;
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