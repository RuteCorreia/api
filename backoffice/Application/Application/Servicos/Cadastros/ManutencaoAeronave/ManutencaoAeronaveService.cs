using Application.DTOs.Cadastros.ManutencaoAeronave.Interface;
using Application.DTOs.Cadastros.ManutencaoAeronave.ViewModel;
using Application.DTOs.Cadastros.ManutencaoAeronaveItemsRevisao.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.ManutencaoAeronave;
using Domain.Interfaces.Cadastros.ManutencaoAeronaveItemsRevisao;
using Helpers;

namespace Application.Application.Servicos.Cadastros.ManutencaoAeronave;

public class ManutencaoAeronaveService : IManutencaoAeronaveService
{
    private readonly IManutencaoAeronaveRepository _manutencaoAeronaveRepository;
    private readonly IManutencaoAeronaveItemsRevisaoRepository _manutencaoItemsRevisaoRepository;
    private readonly IMapper _mapper;

    public ManutencaoAeronaveService(
        IMapper mapper, 
        IManutencaoAeronaveRepository manutencaoAeronaveRepository,
        IManutencaoAeronaveItemsRevisaoRepository manutencaoItemsRevisaoRepository
    )
    {
        _manutencaoAeronaveRepository = manutencaoAeronaveRepository;
        _manutencaoItemsRevisaoRepository = manutencaoItemsRevisaoRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ManutencaoAeronaveViewModel>> GetAllAsync(string? idEmpresa)
    {
        var idEmpresaAsNumber = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var list = await _manutencaoAeronaveRepository.GetAllAsync(idEmpresaAsNumber);
        return _mapper.Map<IEnumerable<ManutencaoAeronaveViewModel>>(list);
    }

    public async Task<ManutencaoAeronaveViewModel> GetByIdAsync(int id)
    {
        var obj = await _manutencaoAeronaveRepository.GetByIdAsync(id);
        var mappedObj = _mapper.Map<ManutencaoAeronaveViewModel>(obj);
        var itensRevisao = await _manutencaoItemsRevisaoRepository.GetAllByManutencaoAeronaveIdAsync(mappedObj.Id);
        
        mappedObj.ItensRevisao = itensRevisao.Select(x => new ManutencaoAeronaveItemsRevisaoViewModel
        {
            Id = x.Id,
            Item = x.Descricao
        });

        if (mappedObj.Documento is not null)
            mappedObj.DocumentoBase64 = ConvertToBase64StringAndReturnImageConcatenaded(mappedObj.Documento);

        if (mappedObj.FichaInspecao is not null)
            mappedObj.FichaInspecaoBase64 = ConvertToBase64StringAndReturnImageConcatenaded(mappedObj.FichaInspecao);

        if (mappedObj.ManualAeronave is not null)
            mappedObj.ManualAeronaveBase64 = ConvertToBase64StringAndReturnImageConcatenaded(mappedObj.ManualAeronave);

        if (mappedObj.MapaComponentes is not null)
            mappedObj.MapaComponentesBase64 = ConvertToBase64StringAndReturnImageConcatenaded(mappedObj.MapaComponentes);

        return mappedObj;
    }

    private string? ConvertToBase64StringAndReturnImageConcatenaded(byte[] doc)
    {
        var base64Img = Convert.ToBase64String(doc);
        var base64Append = $"data:image/jpeg;base64,{base64Img}";
        return base64Append;
    }

    public async Task AddAsync(ManutencaoAeronaveViewModel obj, string? idEmpresa)
    {
        var idEmpresaAsNumber = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var docBase64 = ConvertBase64StringToByteArray(obj.DocumentoBase64);
        var fichaInspecaoBase64 = ConvertBase64StringToByteArray(obj.FichaInspecaoBase64);
        var manualAeronaveBase64 = ConvertBase64StringToByteArray(obj.ManualAeronaveBase64);
        var mapaComponentesBase64 = ConvertBase64StringToByteArray(obj.MapaComponentesBase64);
        if (docBase64 is not null)
            obj.Documento = docBase64;
        if(fichaInspecaoBase64 is not null)
            obj.FichaInspecao = fichaInspecaoBase64;
        if(manualAeronaveBase64 is not null)
            obj.ManualAeronave = manualAeronaveBase64;
        if(mapaComponentesBase64 is not null)
            obj.MapaComponentes = mapaComponentesBase64;

        var mapManutencaoAeronave = _mapper.Map<Domain.Entidades.Cadastros.ManutencaoAeronave.ManutencaoAeronave>(obj);
        mapManutencaoAeronave.IdEmpresa = idEmpresaAsNumber == 0 ? null : idEmpresaAsNumber;
        var objManutencao = await _manutencaoAeronaveRepository.AddAsync(mapManutencaoAeronave);

        if (obj.ItensRevisao.Any())
        {
            var itens = obj.ItensRevisao.Select(x => new Domain.Entidades.Cadastros.ManutencaoAeronaveItemsRevisao.ManutencaoAeronaveItemsRevisao
            {
                Descricao = x.Item,
                IdManutencaoAeronave = objManutencao.Id
            });

            await _manutencaoItemsRevisaoRepository.AddAsync(itens);
        }
    }

    private byte[]? ConvertBase64StringToByteArray(string? doc)
    {
        byte[]? imageDataBytes = null;
        if (!string.IsNullOrEmpty(doc))
        {
            string[] parts = doc.Split(',');
            string decodedBase64String = parts[1];
            imageDataBytes = Convert.FromBase64String(decodedBase64String);
        }
        
        return imageDataBytes;
    } 

    public async Task UpdateAsync(ManutencaoAeronaveViewModel obj)
    {
        var docBase64 = ConvertBase64StringToByteArray(obj.DocumentoBase64);
        var fichaInspecaoBase64 = ConvertBase64StringToByteArray(obj.FichaInspecaoBase64);
        var manualAeronaveBase64 = ConvertBase64StringToByteArray(obj.ManualAeronaveBase64);
        var mapaComponentesBase64 = ConvertBase64StringToByteArray(obj.MapaComponentesBase64);
        if (docBase64 is not null)
            obj.Documento = docBase64;
        if (fichaInspecaoBase64 is not null)
            obj.FichaInspecao = fichaInspecaoBase64;
        if (manualAeronaveBase64 is not null)
            obj.ManualAeronave = manualAeronaveBase64;
        if (mapaComponentesBase64 is not null)
            obj.MapaComponentes = mapaComponentesBase64;
        var mapManutencaoAeronave = _mapper.Map<Domain.Entidades.Cadastros.ManutencaoAeronave.ManutencaoAeronave>(obj);
        await _manutencaoAeronaveRepository.UpdateAsync(mapManutencaoAeronave);
        await _manutencaoItemsRevisaoRepository.DeleteByIdManutencaoAeronaveAsync(obj.Id);
        if (obj.ItensRevisao.Any())
        {
            var itens = obj.ItensRevisao.Select(x => new Domain.Entidades.Cadastros.ManutencaoAeronaveItemsRevisao.ManutencaoAeronaveItemsRevisao
            {
                Descricao = x.Item,
                IdManutencaoAeronave = obj.Id
            });

            await _manutencaoItemsRevisaoRepository.AddAsync(itens);
        }
    }

    public async Task DeleteAsync(int id)
    {
        await _manutencaoAeronaveRepository.DeleteAsync(id);
    }
}
