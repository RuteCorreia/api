using Application.DTOs.Cadastros.AplicacaoAreaTratada.ViewModel;
using Application.DTOs.Cadastros.CaracteristicasProdutoAplicado.Interface;
using Application.DTOs.Cadastros.CaracteristicasProdutoAplicado.ViewModel;
using Application.DTOs.Cadastros.DataFormat.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.CaracteristicasProdutoAplicado;
using Helpers;
using Newtonsoft.Json;

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

    public async Task<int> AddAsync(ProdutoAplicadoViewModel obj, string? idEmpresa)
    {
        
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var mapObj = _mapper.Map<Domain.Entidades.Cadastros.CaracteristicasProdutoAplicado.CaracteristicasProdutoAplicado>(obj);
        mapObj.IdEmpresa = idEmpresaInt == 0 ? null : idEmpresaInt;

        if (obj.Id > 0)
        {
            await _caracteristicasProdutoAplicadoRepository.UpdateAsync(mapObj);
            return mapObj.Id;
        }
        else
        {
            var caracteristicasProdutoAplicado = await _caracteristicasProdutoAplicadoRepository.AddAsync(mapObj);
            return caracteristicasProdutoAplicado;
        }
        
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

    public async Task<ProdutoAplicadoViewModel> GetByIdAsync(int id, string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var obj = await _caracteristicasProdutoAplicadoRepository.GetByIdAsync(id, idEmpresaInt);
        return _mapper.Map<ProdutoAplicadoViewModel>(obj);
    }

    public async Task UpdateAsync(CaracteristicasProdutoAplicadoViewModel obj)
    {
        var mapObj = _mapper.Map<Domain.Entidades.Cadastros.CaracteristicasProdutoAplicado.CaracteristicasProdutoAplicado>(obj);
        await _caracteristicasProdutoAplicadoRepository.UpdateAsync(mapObj);
    }

    public async Task<CaracteristicasProdutoAplicadoViewModel> GetForExportExcelAsync(int id)
    {
        var obj = await _caracteristicasProdutoAplicadoRepository.GetForExportExcelAsync(id);
        return _mapper.Map<CaracteristicasProdutoAplicadoViewModel>(obj);
    }

    public async Task<DataFormatViewModel> GetReceituarioAgronomicoAsync(int? id)
    {
        var obj = await _caracteristicasProdutoAplicadoRepository.GetReceituarioAgronomicoAsync(id);
        var result = JsonConvert.DeserializeObject<DataFormatViewModel>(obj);
        return result;

    }
}
