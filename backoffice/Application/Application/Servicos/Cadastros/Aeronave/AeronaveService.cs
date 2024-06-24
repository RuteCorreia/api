using Application.DTOs.Cadastros.Aeronave.Interface;
using Application.DTOs.Cadastros.Aeronave.ViewModel;
using AutoMapper;
using Domain.Enums;
using Domain.Interfaces.Cadastros.Aeronave;
using Domain.Interfaces.Cadastros.Empresa;
using Helpers;
using System.Collections.Generic;
using System.Text;

namespace Application.Application.Servicos.Cadastros.Aeronave;

public class AeronaveService : IAeronaveService
{
    private readonly IAeronaveRepository _aeronaveRepository;
    private readonly IEmpresaRepository _empresaRepository;
    private readonly IMapper _mapper;

    public AeronaveService(IMapper mapper, IAeronaveRepository aeronaveRepository, IEmpresaRepository empresaRepository)
    {
        _aeronaveRepository = aeronaveRepository;
        _empresaRepository = empresaRepository;
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

    public async Task<(bool, string)> AddAsync(AeronaveViewModel obj, string? idEmpresa)
    {
        var resultMsg = new StringBuilder();

        var mapAeronave = _mapper.Map<Domain.Entidades.Cadastros.Aeronave.Aeronave>(obj);
        var idEmpresaAsNumber = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);

        var empresa = await _empresaRepository.GetByIdAsync(idEmpresaAsNumber);
        var limit = await _aeronaveRepository.GetAllAsync(idEmpresaAsNumber);
        var limitAeronave = limit.Count(a => a.Tipo == ETipoAeronave.Aeronave);
        if (mapAeronave.Tipo == ETipoAeronave.Aeronave && limitAeronave >= empresa.QtdAeronaves)
            return (false, resultMsg.Append("Você ja cadastrou o limite de aeronaves registrados").ToString());

        var limitDrone = limit.Count(a => a.Tipo == ETipoAeronave.Drone);
        if (mapAeronave.Tipo == ETipoAeronave.Drone && limitDrone >= empresa.QtdDrones)
            return (false, resultMsg.Append("Você ja cadastrou o limite de drones registrados").ToString());

        mapAeronave.IdEmpresa = idEmpresaAsNumber == 0 ? null : idEmpresaAsNumber;
        await _aeronaveRepository.AddAsync(mapAeronave);

        return (true, resultMsg.Append("Aeronave Registrada com Sucesso").ToString());
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
