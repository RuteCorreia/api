using Application.DTOs.Cadastros.Veiculo.Interface;
using Application.DTOs.Cadastros.Veiculo.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.Veiculo;
using Helpers;

namespace Application.Application.Servicos.Cadastros.Veiculo;

public class VeiculoService : IVeiculoService
{
    private readonly IMapper _mapper;
    private readonly IVeiculoRepository _veiculoRepository;

    public VeiculoService(
        IMapper mapper, 
        IVeiculoRepository veiculoRepository
    )
    {
        _mapper = mapper;
        _veiculoRepository = veiculoRepository;
    }

    public async Task AddAsync(VeiculoViewModel obj, string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        obj.Placa = obj.Placa.ToUpper();
        var mapObj = _mapper.Map<Domain.Entidades.Cadastros.Veiculo.Veiculo>(obj);
        mapObj.IdEmpresa = idEmpresaInt == 0 ? null : idEmpresaInt;  
        await _veiculoRepository.AddAsync(mapObj);
    }

    public async Task DeleteAsync(int id, string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        await _veiculoRepository.DeleteAsync(id, idEmpresaInt);
    }

    public async Task<IEnumerable<VeiculoViewModel>> GetAllAsync(string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var list = await _veiculoRepository.GetAllAsync(idEmpresaInt);
        return _mapper.Map<IEnumerable<VeiculoViewModel>>(list);
    }

    public async Task<VeiculoViewModel?> GetByIdAsync(int id, string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var obj = await _veiculoRepository.GetByIdAsync(id, idEmpresaInt);
        return _mapper.Map<VeiculoViewModel>(obj);
    }

    public async Task UpdateAsync(VeiculoViewModel obj)
    {
        obj.Placa = obj.Placa.ToUpper();
        var mapObj = _mapper.Map<Domain.Entidades.Cadastros.Veiculo.Veiculo>(obj);
        await _veiculoRepository.UpdateAsync(mapObj);
    }

    public async Task<int?> GetKmAtualByIdAsync(int id, string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var veiculo = await _veiculoRepository.GetByIdAsync(id, idEmpresaInt);
        return veiculo?.KM_Atual;
    }


}
