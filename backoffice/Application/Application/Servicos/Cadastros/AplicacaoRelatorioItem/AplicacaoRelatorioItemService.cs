using Application.DTOs.Cadastros.AplicacaoRelatorio.ViewModel;
using Application.DTOs.Cadastros.AplicacaoRelatorioItem.Interface;
using Application.DTOs.Cadastros.AplicacaoRelatorioItem.ViewModel;
using Application.DTOs.Cadastros.DataFormat.ViewModel;
using AutoMapper;
using Domain.Interfaces.BlobStorage;
using Domain.Interfaces.Cadastros.AplicacaoRelatorioItem;
using Newtonsoft.Json;

namespace Application.Application.Servicos.Cadastros.AplicacaoRelatorioItem;

public class AplicacaoRelatorioItemService : IAplicacaoRelatorioItemService
{
    private readonly IAplicacaoRelatorioItemRepository _aplicacaoRelatorioItemRepository;
    private readonly IBlobStorageRepository _blobStorageRepository;
    private readonly IMapper _mapper;

    public AplicacaoRelatorioItemService(
        IAplicacaoRelatorioItemRepository aplicacaoRelatorioItemRepository,
        IBlobStorageRepository blobStorageRepository,
        IMapper mapper)
    {
        _aplicacaoRelatorioItemRepository = aplicacaoRelatorioItemRepository;
        _blobStorageRepository = blobStorageRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RelatorioItemViewModel>> GetAllAsync(int idRelatorioAplicacao)
    {
        var list = await _aplicacaoRelatorioItemRepository.GetAllByAplicacaoRelatorioIdAsync(idRelatorioAplicacao);
        var aplicacaoRelatorioItemViewModel = _mapper.Map<IEnumerable<RelatorioItemViewModel>>(list);
        foreach (var item in aplicacaoRelatorioItemViewModel)
        {
            if (!string.IsNullOrEmpty(item.ImagemCondicaoClimatica) &&
                item.ImagemCondicaoClimatica != "{\"Format\":\"raw\",\"Data\":null}")
            {
                var fileExtension = Path.GetExtension(item.ImagemCondicaoClimatica)?.ToLower().TrimStart('.');
                var croquiArea = await _blobStorageRepository.GetPdfAsync(item.ImagemCondicaoClimatica);
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

                item.ImagemCondicaoClimatica = JsonConvert.SerializeObject(croquiAreaDataFormat);
            }
        }
        return aplicacaoRelatorioItemViewModel;
    }

    public async Task<IEnumerable<RelatorioItemViewModel>> GetForExportExcelAsync(int id)
    {
        var obj = await _aplicacaoRelatorioItemRepository.GetForExportExcelAsync(id);
        return _mapper.Map<IEnumerable<RelatorioItemViewModel>>(obj);
    }

    public async Task<IEnumerable<RelatorioItemViewModel>> GetAllByAplicacaoRelatorioIdAsync(int aplicacaoRelatorioId)
    {
        var list = await _aplicacaoRelatorioItemRepository.GetAllByAplicacaoRelatorioIdAsync(aplicacaoRelatorioId);
        var aplicacaoRelatorioItemViewModel = _mapper.Map<IEnumerable<RelatorioItemViewModel>>(list);
        foreach (var item in aplicacaoRelatorioItemViewModel) 
        {
            if (!string.IsNullOrEmpty(item.ImagemCondicaoClimatica) &&
                item.ImagemCondicaoClimatica != "{\"Format\":\"raw\",\"Data\":null}")
            {
                var fileExtension = Path.GetExtension(item.ImagemCondicaoClimatica)?.ToLower().TrimStart('.');
                var croquiArea = await _blobStorageRepository.GetPdfAsync(item.ImagemCondicaoClimatica);
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

                item.ImagemCondicaoClimatica = JsonConvert.SerializeObject(croquiAreaDataFormat);
            }
        }
        
        return aplicacaoRelatorioItemViewModel;
    }


    public async Task<RelatorioItemViewModel> GetByIdAsync(int id)
    {
        var obj = await _aplicacaoRelatorioItemRepository.GetByIdAsync(id);
        var aplicacaoRelatorioItemViewModel = _mapper.Map<RelatorioItemViewModel>(obj);
        if (!string.IsNullOrEmpty(aplicacaoRelatorioItemViewModel.ImagemCondicaoClimatica) &&
            aplicacaoRelatorioItemViewModel.ImagemCondicaoClimatica != "{\"Format\":\"raw\",\"Data\":null}")
        {
            var fileExtension = Path.GetExtension(aplicacaoRelatorioItemViewModel.ImagemCondicaoClimatica)?.ToLower().TrimStart('.');
            var croquiArea = await _blobStorageRepository.GetPdfAsync(aplicacaoRelatorioItemViewModel.ImagemCondicaoClimatica);
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

            aplicacaoRelatorioItemViewModel.ImagemCondicaoClimatica = JsonConvert.SerializeObject(croquiAreaDataFormat);
        }
        return aplicacaoRelatorioItemViewModel;
    }

    public async Task AddAsync(AplicacaoRelatorioItemViewModel obj)
    {
        var mapAplicacaoRelatorioItem = _mapper.Map<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem>(obj);
        await _aplicacaoRelatorioItemRepository.AddAsync(mapAplicacaoRelatorioItem);
    }

    //public async Task UpdateAsync(List<AplicacaoRelatorioItemViewModel> objs)
    //{
    //    foreach (var obj in objs)
    //    {
    //        var mapAplicacaoRelatorioItem = _mapper.Map<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem>(obj);
    //        await _aplicacaoRelatorioItemRepository.UpdateAsync(mapAplicacaoRelatorioItem);
    //    }
    //}

    public async Task DeleteAsync(int id)
    {
        await _aplicacaoRelatorioItemRepository.DeleteAsync(id);
    }
}
