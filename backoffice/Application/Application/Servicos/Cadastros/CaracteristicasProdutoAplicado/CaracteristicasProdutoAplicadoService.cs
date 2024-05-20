using Application.DTOs.Cadastros.CaracteristicasProdutoAplicado.Interface;
using Application.DTOs.Cadastros.CaracteristicasProdutoAplicado.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.CaracteristicasProdutoAplicado;
using Helpers;

namespace Application.Application.Servicos.Cadastros.CaracteristicasProdutoAplicado;

public class CaracteristicasProdutoAplicadoService : ICaracteristicasProdutoAplicadoService
{
    private readonly ICaracteristicasProdutoAplicadoRepository _caracteristicasProdutoAplicadoRepository;
    private readonly IMapper _mapper;

    public CaracteristicasProdutoAplicadoService(
        IMapper mapper,
        ICaracteristicasProdutoAplicadoRepository caracteristicasProdutoAplicadoRepository
    )
    {
        _mapper = mapper;
        _caracteristicasProdutoAplicadoRepository = caracteristicasProdutoAplicadoRepository;
    }

    public async Task AddAsync(CaracteristicasProdutoAplicadoViewModel obj, string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var mapObj = _mapper.Map<Domain.Entidades.Cadastros.CaracteristicasProdutoAplicado.CaracteristicasProdutoAplicado>(obj);
        mapObj.IdEmpresa = idEmpresaInt == 0 ? null : idEmpresaInt;
        await _caracteristicasProdutoAplicadoRepository.AddAsync(mapObj);
    }

    public async Task DeleteAsync(int id, string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        await _caracteristicasProdutoAplicadoRepository.DeleteAsync(id, idEmpresaInt);
    }

    public async Task<IEnumerable<CaracteristicasProdutoAplicadoViewModel>> GetAllAsync(string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var list = await _caracteristicasProdutoAplicadoRepository.GetAllAsync(idEmpresaInt);
        return _mapper.Map<IEnumerable<CaracteristicasProdutoAplicadoViewModel>>(list);
    }

    public async Task<CaracteristicasProdutoAplicadoViewModel> GetByIdAsync(int id, string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var obj = await _caracteristicasProdutoAplicadoRepository.GetByIdAsync(id, idEmpresaInt);
        return _mapper.Map<CaracteristicasProdutoAplicadoViewModel>(obj);
    }

    public async Task UpdateAsync(CaracteristicasProdutoAplicadoViewModel obj)
    {
        var mapObj = _mapper.Map<Domain.Entidades.Cadastros.CaracteristicasProdutoAplicado.CaracteristicasProdutoAplicado>(obj);
        await _caracteristicasProdutoAplicadoRepository.UpdateAsync(mapObj);
    }
}
