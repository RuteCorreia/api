using Application.DTOs.Cadastros.AplicacaoAreaTratada.ViewModel;
using Application.DTOs.Cadastros.AplicacaoRelatorio.Interface;
using Application.DTOs.Cadastros.AplicacaoRelatorio.ViewModel;
using Application.DTOs.Cadastros.DataFormat.ViewModel;
using AutoMapper;
using Domain.Interfaces.BlobStorage;
using Domain.Interfaces.Cadastros.AplicacaoRelatorio;
using Domain.Interfaces.Cadastros.AplicacaoRelatorioItem;
using Helpers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json;

namespace Application.Application.Servicos.Cadastros.AplicacaoRelatorio;

public class AplicacaoRelatorioService : IAplicacaoRelatorioService
{
    private readonly IAplicacaoRelatorioItemRepository _aplicacaoRelatorioItemRepository;
    private readonly IAplicacaoRelatorioRepository _aplicacaoRelatorioRepository;
    private readonly IBlobStorageRepository _blobStorageRepository;
    private readonly IMapper _mapper;

    public AplicacaoRelatorioService(
        IAplicacaoRelatorioRepository aplicacaoRelatorioRepository,
        IAplicacaoRelatorioItemRepository aplicacaoRelatorioItemRepository,
        IBlobStorageRepository blobStorageRepository,
        IMapper mapper 
        )
    {
        _aplicacaoRelatorioItemRepository = aplicacaoRelatorioItemRepository;
        _aplicacaoRelatorioRepository = aplicacaoRelatorioRepository;
        _blobStorageRepository = blobStorageRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AplicacaoRelatorioViewModel>> GetAllAsync()
    {
        var list = await _aplicacaoRelatorioRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<AplicacaoRelatorioViewModel>>(list);
    }

    public async Task<AplicacaoRelatorioViewModel> GetByIdAsync(int id)
    {
        var obj = await _aplicacaoRelatorioRepository.GetByIdAsync(id);
        if (obj == null)
        {
            return null;
        }

        List<DataFormatViewModel> mapaAplicado = null;

        if (!string.IsNullOrEmpty(obj.MapaAplicacao))
        {
            mapaAplicado = JsonConvert.DeserializeObject<List<DataFormatViewModel>>(obj.MapaAplicacao);
        }
        
        var aplicacaoRelatorio = _mapper.Map<AplicacaoRelatorioViewModel>(obj);
        aplicacaoRelatorio.MapaAplicado = mapaAplicado;

        return aplicacaoRelatorio;
    }

    public async Task<AplicacaoRelatorioViewModel> GetForExportExcelAsync(int id)
    {
        var obj = await _aplicacaoRelatorioRepository.GetForExportExcelAsync(id);
        return _mapper.Map<AplicacaoRelatorioViewModel>(obj);
    }
    public async Task<int> AddAsync(StringAplicacaoRelatorioViewModel obj, string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var mapaAplicado = JsonConvert.SerializeObject(obj.MapaAplicado);

        var mapAplicacaoRelatorio = _mapper.Map<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorio>(obj);
        mapAplicacaoRelatorio.MapaAplicacao = mapaAplicado;
        mapAplicacaoRelatorio.IdEmpresa = idEmpresaInt == 0 ? null : idEmpresaInt;
        List<DataFormatViewModel> mapaAplicadoDataFormat;
        List<string> fileNames = new List<string>();

        if (!string.IsNullOrEmpty(mapAplicacaoRelatorio.MapaAplicacao))
        {
            mapaAplicadoDataFormat = JsonConvert.DeserializeObject<List<DataFormatViewModel>>(mapAplicacaoRelatorio.MapaAplicacao);
            foreach(var item in mapaAplicadoDataFormat)
            {
                if (!string.IsNullOrEmpty(item.Data))
                {
                    byte[] mapaAplicadoBytes = Convert.FromBase64String(item.Data);

                    string fileName = $"MapaAplicado - {Guid.NewGuid()}.{item.Format}";
                    using (var stream = new MemoryStream(mapaAplicadoBytes))
                    {
                        await _blobStorageRepository.SavePdfAsync(stream, fileName);
                    }

                    fileNames.Add(fileName);
                }
            }
        }

        mapAplicacaoRelatorio.MapaAplicacao = JsonConvert.SerializeObject(fileNames);

        if (obj.Id > 0)
        {
            await _aplicacaoRelatorioRepository.UpdateAsync(mapAplicacaoRelatorio);
        }
        else
        {
            await _aplicacaoRelatorioRepository.AddAsync(mapAplicacaoRelatorio);
        }

        if (obj.Aplicacoes != null && obj.Aplicacoes.Any())
        {
            foreach (var aplicacaoItem in obj.Aplicacoes)
            {
                var mapAplicacaoRelatorioItem = _mapper.Map<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem>(aplicacaoItem);
                mapAplicacaoRelatorioItem.IdAplicacaoRelatorio = mapAplicacaoRelatorio.Id; // Atribua o Id da entidade principal
                mapAplicacaoRelatorioItem.IdEmpresa = idEmpresaInt == 0 ? null : idEmpresaInt;
                DataFormatViewModel dadosClimaticosDataFormat;
                if (!string.IsNullOrEmpty(mapAplicacaoRelatorioItem.ImagemDadosClimaticos))
                {
                    dadosClimaticosDataFormat = JsonConvert.DeserializeObject<DataFormatViewModel>(mapAplicacaoRelatorioItem.ImagemDadosClimaticos);
                    if (!string.IsNullOrEmpty(dadosClimaticosDataFormat.Data))
                    {
                        byte[] dadosClimaticosBytes = Convert.FromBase64String(dadosClimaticosDataFormat.Data);

                        string fileName = $"ReceituarioAgronomico - {Guid.NewGuid()}.{dadosClimaticosDataFormat.Format}";
                        using (var stream = new MemoryStream(dadosClimaticosBytes))
                        {
                            await _blobStorageRepository.SavePdfAsync(stream, fileName);
                        }

                        mapAplicacaoRelatorioItem.ImagemDadosClimaticos = fileName;
                    }
                }

                if (aplicacaoItem.Id > 0)
                {
                    await _aplicacaoRelatorioItemRepository.UpdateAsync(mapAplicacaoRelatorioItem);
                }
                else
                {
                    await _aplicacaoRelatorioItemRepository.AddAsync(mapAplicacaoRelatorioItem);
                }
            }
        }
        
        return mapAplicacaoRelatorio.Id;
        
    }

    public async Task UpdateAsync(AplicacaoRelatorioViewModel obj)
    {
        var mapAplicacaoRelatorio = _mapper.Map<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorio>(obj);
        await _aplicacaoRelatorioRepository.UpdateAsync(mapAplicacaoRelatorio);
    }

    public async Task DeleteAsync(int id)
    {
        await _aplicacaoRelatorioRepository.DeleteAsync(id);
    }
}
