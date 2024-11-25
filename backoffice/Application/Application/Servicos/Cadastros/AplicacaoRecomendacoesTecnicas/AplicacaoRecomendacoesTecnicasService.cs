using Application.DTOs.Cadastros.AplicacaoAreaTratada.ViewModel;
using Application.DTOs.Cadastros.AplicacaoRecomendacoesTecnicas.Interface;
using Application.DTOs.Cadastros.AplicacaoRecomendacoesTecnicas.ViewModel;
using Application.DTOs.Cadastros.DataFormat.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.AplicacaoRecomendacoesTecnicas;
using Helpers;
using Newtonsoft.Json;

namespace Application.Application.Servicos.Cadastros.AplicacaoRecomendacoesTecnicas;

public class AplicacaoRecomendacoesTecnicasService : IAplicacaoRecomendacoesTecnicasService
{
    private readonly IAplicacaoRecomendacoesTecnicasRepository _aplicacaoRecomendacoesTecnicasRepository;
    private readonly IMapper _mapper;

    public AplicacaoRecomendacoesTecnicasService(IMapper mapper, IAplicacaoRecomendacoesTecnicasRepository aplicacaoRecomendacoesTecnicasRepository)
    {
        _aplicacaoRecomendacoesTecnicasRepository = aplicacaoRecomendacoesTecnicasRepository;
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
        return _mapper.Map<AplicacaoRecomendacoesTecnicasViewModel>(obj);
    }

    public async Task<int> AddAsync(AplicacaoRecomendacoesTecnicasViewModel obj, string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var mapAplicacaoRecomendacoesTecnicas = _mapper.Map<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRecomendacoesTecnicas>(obj);
        mapAplicacaoRecomendacoesTecnicas.IdEmpresa = idEmpresaInt == 0 ? null : idEmpresaInt;
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
