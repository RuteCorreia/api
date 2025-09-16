using Application.DTOs.Cadastros.AplicacaoAreaTratada.ViewModel;
using Application.DTOs.Cadastros.AplicacaoRecomendacoesTecnicas.Interface;
using Application.DTOs.Cadastros.AplicacaoRecomendacoesTecnicas.ViewModel;
using Application.DTOs.Cadastros.DataFormat.ViewModel;
using AutoMapper;
using Domain.Interfaces.BlobStorage;
using Domain.Interfaces.Cadastros.AplicacaoRecomendacoesTecnicas;
using Helpers;
using Newtonsoft.Json;

namespace Application.Application.Servicos.Cadastros.AplicacaoRecomendacoesTecnicas;

public class AplicacaoRecomendacoesTecnicasService : IAplicacaoRecomendacoesTecnicasService
{
    private readonly IAplicacaoRecomendacoesTecnicasRepository _aplicacaoRecomendacoesTecnicasRepository;
    private readonly IBlobStorageRepository _blobStorageRepository;
    private readonly IMapper _mapper;

    public AplicacaoRecomendacoesTecnicasService(IMapper mapper, 
        IAplicacaoRecomendacoesTecnicasRepository aplicacaoRecomendacoesTecnicasRepository,
        IBlobStorageRepository blobStorageRepository)
    {
        _aplicacaoRecomendacoesTecnicasRepository = aplicacaoRecomendacoesTecnicasRepository;
        _blobStorageRepository = blobStorageRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AplicacaoRecomendacoesTecnicasViewModel>> GetAllAsync()
    {
        var list = await _aplicacaoRecomendacoesTecnicasRepository.GetAllAsync();
        var recomendacoesTecnicasViewModel = _mapper.Map<IEnumerable<AplicacaoRecomendacoesTecnicasViewModel>>(list);
        foreach (var item in recomendacoesTecnicasViewModel)
        {
            if(!string.IsNullOrEmpty(item.ArquivoDrone))
            item.ArquivoDroneDataFormat = JsonConvert.DeserializeObject<DataFormatViewModel>(item.ArquivoDrone);
        }
        return recomendacoesTecnicasViewModel;

    }

    public async Task<AplicacaoRecomendacoesTecnicasViewModel> GetForExportExcelAsync(int id)
    {
        var obj = await _aplicacaoRecomendacoesTecnicasRepository.GetForExportExcelAsync(id);
        return _mapper.Map<AplicacaoRecomendacoesTecnicasViewModel>(obj);
    }
    public async Task<AplicacaoRecomendacoesTecnicasViewModel> GetByIdAsync(int id)
    {
        var obj = await _aplicacaoRecomendacoesTecnicasRepository.GetByIdAsync(id);
        var recomendacoesTecnicasViewModel = _mapper.Map<AplicacaoRecomendacoesTecnicasViewModel>(obj);
        if (!string.IsNullOrEmpty(recomendacoesTecnicasViewModel.ArquivoDrone) &&
            recomendacoesTecnicasViewModel.ArquivoDrone != "{\"Format\":\"raw\",\"Data\":null}")
        {
            var fileExtension = Path.GetExtension(recomendacoesTecnicasViewModel.ArquivoDrone)?.ToLower().TrimStart('.');
            var croquiArea = await _blobStorageRepository.GetPdfAsync(recomendacoesTecnicasViewModel.ArquivoDrone);
            string croquiAreaBase64 = "";
            using (var memoryStream = new MemoryStream())
            {
                await croquiArea.CopyToAsync(memoryStream);
                var byteArray = memoryStream.ToArray();
                croquiAreaBase64 = Convert.ToBase64String(byteArray);
            }

            var croquiAreaDataFormat = new DataFormatViewModel
            {
                Format = fileExtension,
                Data = croquiAreaBase64
            };

            recomendacoesTecnicasViewModel.ArquivoDrone = JsonConvert.SerializeObject(croquiAreaDataFormat);
            recomendacoesTecnicasViewModel.ArquivoDroneDataFormat = JsonConvert.DeserializeObject<DataFormatViewModel>(recomendacoesTecnicasViewModel.ArquivoDrone);
        }
           
        return recomendacoesTecnicasViewModel;
    }

    public async Task<int> AddAsync(AplicacaoRecomendacoesTecnicasViewModel obj, string? idEmpresa)
    {
        var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
        var mapAplicacaoRecomendacoesTecnicas = _mapper.Map<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRecomendacoesTecnicas>(obj);
        mapAplicacaoRecomendacoesTecnicas.IdEmpresa = idEmpresaInt == 0 ? null : idEmpresaInt;
        DataFormatViewModel arquivoDroneDataFormat;
        if (!string.IsNullOrEmpty(mapAplicacaoRecomendacoesTecnicas.ArquivoDrone))
        {
            arquivoDroneDataFormat = JsonConvert.DeserializeObject<DataFormatViewModel>(mapAplicacaoRecomendacoesTecnicas.ArquivoDrone);
            if (!string.IsNullOrEmpty(arquivoDroneDataFormat.Data))
            {
                byte[] croquiAreaBytes = Convert.FromBase64String(arquivoDroneDataFormat.Data);

                string fileName = $"ArquivoDrone - {Guid.NewGuid()}.{arquivoDroneDataFormat.Format}";
                using (var stream = new MemoryStream(croquiAreaBytes))
                {
                    await _blobStorageRepository.SavePdfAsync(stream, fileName);
                }

                mapAplicacaoRecomendacoesTecnicas.ArquivoDrone = fileName;
            }
        }
        if (obj.Id > 0)
        {
            await _aplicacaoRecomendacoesTecnicasRepository.UpdateAsync(mapAplicacaoRecomendacoesTecnicas);
            return mapAplicacaoRecomendacoesTecnicas.Id;
        }
        else
        {
            var aplicacaoRecomendacoesTecnicas = _aplicacaoRecomendacoesTecnicasRepository.AddAsync(mapAplicacaoRecomendacoesTecnicas);
            return aplicacaoRecomendacoesTecnicas.Result;
        }   
    }

    public async Task UpdateAsync(AplicacaoRecomendacoesTecnicasViewModel obj)
    {
        var mapAplicacaoRecomendacoesTecnicas = _mapper.Map<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRecomendacoesTecnicas>(obj);
        await _aplicacaoRecomendacoesTecnicasRepository.UpdateAsync(mapAplicacaoRecomendacoesTecnicas);
    }

    public async Task DeleteAsync(int id)
    {
        await _aplicacaoRecomendacoesTecnicasRepository.DeleteAsync(id);
    }
}
