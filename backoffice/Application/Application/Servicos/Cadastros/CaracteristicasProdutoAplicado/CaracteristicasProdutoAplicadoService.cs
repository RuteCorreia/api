using Application.DTOs.Cadastros.AplicacaoAreaTratada.ViewModel;
using Application.DTOs.Cadastros.CaracteristicasProdutoAplicado.Interface;
using Application.DTOs.Cadastros.CaracteristicasProdutoAplicado.ViewModel;
using Application.DTOs.Cadastros.DataFormat.ViewModel;
using AutoMapper;
using Domain.Entidades.Cadastros.CaracteristicasProdutoAplicado;
using Domain.Interfaces.BlobStorage;
using Domain.Interfaces.Cadastros.CaracteristicasProdutoAplicado;
using Helpers;
using Newtonsoft.Json;

namespace Application.Application.Servicos.Cadastros.CaracteristicasProdutoAplicado;

public class CaracteristicasProdutoAplicadoService : ICaracteristicasProdutoAplicadoService
{
    private readonly ICaracteristicasProdutoAplicadoRepository _caracteristicasProdutoAplicadoRepository;
    private readonly IBlobStorageRepository _blobStorageRepository;
    private readonly IMapper _mapper;

    public CaracteristicasProdutoAplicadoService(
        IMapper mapper,
        IBlobStorageRepository blobStorageRepository,
        ICaracteristicasProdutoAplicadoRepository caracteristicasProdutoAplicadoRepository
    )
    {
        _mapper = mapper;
        _blobStorageRepository = blobStorageRepository;
        _caracteristicasProdutoAplicadoRepository = caracteristicasProdutoAplicadoRepository;
    }

    public async Task<int> AddAsync(ProdutoAplicadoViewModel obj, string? idEmpresa)
    {
        
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var mapObj = _mapper.Map<Domain.Entidades.Cadastros.CaracteristicasProdutoAplicado.CaracteristicasProdutoAplicado>(obj);
        mapObj.IdEmpresa = idEmpresaInt == 0 ? null : idEmpresaInt;
        DataFormatViewModel receituarioDataFormat;
        if (!string.IsNullOrEmpty(mapObj.ReceiturarioAgronomico))
        {
            receituarioDataFormat = JsonConvert.DeserializeObject<DataFormatViewModel>(mapObj.ReceiturarioAgronomico);
            if (!string.IsNullOrEmpty(receituarioDataFormat.Data))
            {
                byte[] receituarioAgronomicoBytes = Convert.FromBase64String(receituarioDataFormat.Data);

                string fileName = $"ReceituarioAgronomico - {Guid.NewGuid()}.{receituarioDataFormat.Format}";
                using (var stream = new MemoryStream(receituarioAgronomicoBytes))
                {
                    await _blobStorageRepository.SavePdfAsync(stream, fileName);
                }

                mapObj.ReceiturarioAgronomico = fileName;
            }
        }

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
        var mapCaracteristicasProdutoAplicado = _mapper.Map<ProdutoAplicadoViewModel>(obj);
        if (!string.IsNullOrEmpty(mapCaracteristicasProdutoAplicado.ReceiturarioAgronomico) &&
            mapCaracteristicasProdutoAplicado.ReceiturarioAgronomico != "{\"Format\":\"raw\",\"Data\":null}")
        {
            var fileExtension = Path.GetExtension(mapCaracteristicasProdutoAplicado.ReceiturarioAgronomico)?.ToLower().TrimStart('.');
            var receiturarioAgronomico = await _blobStorageRepository.GetPdfAsync(mapCaracteristicasProdutoAplicado.ReceiturarioAgronomico);
            string receiturarioAgronomicoBase64 = "";
            using (var memoryStream = new MemoryStream())
            {
                await receiturarioAgronomico.CopyToAsync(memoryStream);
                var byteArray = memoryStream.ToArray();
                receiturarioAgronomicoBase64 = Convert.ToBase64String(byteArray);
            }

            var croquiAreaDataFormat = new DataFormatViewModel
            {
                Format = fileExtension,
                Data = receiturarioAgronomicoBase64
            };

            mapCaracteristicasProdutoAplicado.ReceiturarioAgronomico = JsonConvert.SerializeObject(croquiAreaDataFormat);
        }
        return mapCaracteristicasProdutoAplicado;
    }

    public async Task UpdateAsync(CaracteristicasProdutoAplicadoViewModel obj)
    {
        var mapObj = _mapper.Map<Domain.Entidades.Cadastros.CaracteristicasProdutoAplicado.CaracteristicasProdutoAplicado>(obj);
        await _caracteristicasProdutoAplicadoRepository.UpdateAsync(mapObj);
    }

    public async Task AdicionarReceituarioAgronomicoAsync(int id, DataFormatViewModel objData)
    {
        var receituario = "";
        if (!string.IsNullOrEmpty(objData.Data))
        {
            byte[] receituarioAgronomicoBytes = Convert.FromBase64String(objData.Data);

            string fileName = $"ReceituarioAgronomico - {Guid.NewGuid()}.{objData.Format}";
            using (var stream = new MemoryStream(receituarioAgronomicoBytes))
            {
                await _blobStorageRepository.SavePdfAsync(stream, fileName);
            }

            receituario = fileName;
        }
        await _caracteristicasProdutoAplicadoRepository.AdicionarReceituarioAgronomicoAsync(id, receituario);
    }

    public async Task<CaracteristicasProdutoAplicadoViewModel> GetForExportExcelAsync(int id)
    {
        var obj = await _caracteristicasProdutoAplicadoRepository.GetForExportExcelAsync(id);
        return _mapper.Map<CaracteristicasProdutoAplicadoViewModel>(obj);
    }

    public async Task<DataFormatViewModel> GetReceituarioAgronomicoAsync(int? id)
    {
        if (id == null)
        {
            return null; // Retorna null se o ID for nulo
        }
        var obj = await _caracteristicasProdutoAplicadoRepository.GetReceituarioAgronomicoAsync(id);
        var result = new DataFormatViewModel();
        if (!string.IsNullOrEmpty(obj) &&
                            obj != "{\"Format\":\"raw\",\"Data\":null}")
        {
            string extension = Path.GetExtension(obj).TrimStart('.');
            var data = await _blobStorageRepository.GetPdfAsync(obj);
            using (var memoryStream = new MemoryStream())
            {
                await data.CopyToAsync(memoryStream);
                var fileBytes = memoryStream.ToArray();

                string base64String = Convert.ToBase64String(fileBytes);

                result.Data = base64String;
                result.Format = extension;
            }

            return result;
        }
        return null;

    }
}
