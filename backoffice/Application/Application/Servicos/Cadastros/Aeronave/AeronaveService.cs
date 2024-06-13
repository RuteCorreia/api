using Application.DTOs.Cadastros.Aeronave.Interface;
using Application.DTOs.Cadastros.Aeronave.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.Aeronave;
using Helpers;
using System.Collections.Generic;

namespace Application.Application.Servicos.Cadastros.Aeronave;

public class AeronaveService : IAeronaveService
{
    private readonly IAeronaveRepository _aeronaveRepository;
    private readonly IMapper _mapper;

    public AeronaveService(IMapper mapper, IAeronaveRepository aeronaveRepository)
    {
        _aeronaveRepository = aeronaveRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AeronaveViewModel>> GetAllAsync(string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var list = await _aeronaveRepository.GetAllAsync(idEmpresaInt);
        return _mapper.Map<IEnumerable<AeronaveViewModel>>(list);
    }

    public async Task<AeronaveViewModel> GetByIdAsync(int id)
    {
        var obj = await _aeronaveRepository.GetByIdAsync(id);
        return _mapper.Map<AeronaveViewModel>(obj);
    }

    public async Task AddAsync(AeronaveViewModel obj, string? idEmpresa)
    {
        var mapAeronave = _mapper.Map<Domain.Entidades.Cadastros.Aeronave.Aeronave>(obj);
        var idEmpresaAsNumber = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        mapAeronave.IdEmpresa = idEmpresaAsNumber == 0 ? null : idEmpresaAsNumber;
        await _aeronaveRepository.AddAsync(mapAeronave);
    }

    public async Task UpdateAsync(AeronaveViewModel obj)
    {
        var mapAeronave = _mapper.Map<Domain.Entidades.Cadastros.Aeronave.Aeronave>(obj);
        await _aeronaveRepository.UpdateAsync(mapAeronave);
    }

    public async Task<IEnumerable<AeronaveViewModel>> GetByNameAsync(string name, string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var list = await _aeronaveRepository.GetByNameAsync(name, idEmpresaInt);
        return _mapper.Map<IEnumerable<AeronaveViewModel>>(list);
    }

    public async Task DeleteAsync(int id)
    {
        await _aeronaveRepository.DeleteAsync(id);
    }
}
