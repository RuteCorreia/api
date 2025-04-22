using Application.DTOs.Cadastros.Controle_De_Frota.Interface;
using Application.DTOs.Cadastros.Controle_De_Frota.ViewModel;
using Application.DTOs.Cadastros.DataFormat.ViewModel;
using AutoMapper;
using Domain.Interfaces.BlobStorage;
using Domain.Interfaces.Cadastros.ControleDeFrota;
using Domain.Interfaces.Cadastros.Veiculo;
using Domain.Interfaces.User;
using Helpers;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Application.Application.Servicos.Cadastros.ControleDeFrota;

public class ControleDeFrotaService : IControleDeFrotaService
{
    private readonly IControleDeFrotaRepository _controleDeFrotaRepository;
    private readonly IBlobStorageRepository _blobStorageRepository;
    private readonly IVeiculoRepository _veiculoRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IMapper _mapper;

    public ControleDeFrotaService( 
        IControleDeFrotaRepository controleDeFrotaRepository,
        IBlobStorageRepository blobStorageRepository,
        IVeiculoRepository veiculoRepository,
        IUsuarioRepository usuarioRepository,
        IMapper mapper)
    {
        _controleDeFrotaRepository = controleDeFrotaRepository;
        _blobStorageRepository = blobStorageRepository;
        _veiculoRepository = veiculoRepository;
        _usuarioRepository = usuarioRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ControleDeFrotaViewModel>> GetAllAsync(DateTime? offsetDate, string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var list = await _controleDeFrotaRepository.GetAllAsync(offsetDate, idEmpresaInt);

        foreach (var item in list)
        {
            if (!item.Imagem.IsNullOrEmpty())
            {
                var imagemFrotas = await _blobStorageRepository.GetPdfAsync(item.Imagem);
                string imagemBase64 = "";
                using (var memoryStream = new MemoryStream())
                {
                    await imagemFrotas.CopyToAsync(memoryStream);
                    var byteArray = memoryStream.ToArray();
                    imagemBase64 = Convert.ToBase64String(byteArray);
                }

                item.Imagem = imagemBase64;

            }
        }

        return _mapper.Map<IEnumerable<ControleDeFrotaViewModel>>(list);
    }

    public async Task<IEnumerable<ControleDeFrotaViewModel>> GetListByStatusAsync(string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var statusEnvio = 0;
        var list = await _controleDeFrotaRepository.GetListByStatusAsync(idEmpresaInt, statusEnvio);
        return _mapper.Map<IEnumerable<ControleDeFrotaViewModel>>(list);
    }

    public async Task<ControleDeFrotaViewModel> GetByIdAsync(int? id)
    {
        var obj = await _controleDeFrotaRepository.GetByIdAsync(id);
        return _mapper.Map<ControleDeFrotaViewModel>(obj);
    }

    public async Task<IEnumerable<ControleDeFrotaViewModel>> GetListByMesAsync(string? idEmpresa, DateTime primeiroDiaMes, DateTime ultimoDiaMes)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var statusEnvio = 0;
        var list = await _controleDeFrotaRepository.GetListByMesAsync(idEmpresaInt, statusEnvio, primeiroDiaMes, ultimoDiaMes);
        return _mapper.Map<IEnumerable<ControleDeFrotaViewModel>>(list);
    }

    public async Task<IEnumerable<ControleDeFrotaViewModel>> GetListByIdsAsync(string? idEmpresa, List<int> ids, int isMapa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var statusEnvio = 0;
        var list = await _controleDeFrotaRepository.GetListByIdsAsync(ids, idEmpresaInt, statusEnvio, isMapa);
        return _mapper.Map<IEnumerable<ControleDeFrotaViewModel>>(list);
    }

    public async Task<int> AddAsync(ControleDeFrotaViewModel obj, string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var mapControleDeFrota = _mapper.Map<Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>(obj);
        mapControleDeFrota.IdEmpresa = idEmpresaInt == 0 ? null : idEmpresaInt;
        if (!string.IsNullOrEmpty(mapControleDeFrota.Imagem))
        {
            byte[] dataBytes = Convert.FromBase64String(mapControleDeFrota.Imagem);

            string fileName = $"ImagemFrota - {Guid.NewGuid()}.pdf";
            using (var stream = new MemoryStream(dataBytes))
            {
                await _blobStorageRepository.SavePdfAsync(stream, fileName);
            }

            mapControleDeFrota.Imagem = fileName;
        }
        mapControleDeFrota.NomeRelatorio = $"Frota - {mapControleDeFrota.NomeExecutor} - {mapControleDeFrota.NomePiloto} - {mapControleDeFrota.DataCriacao:dd/MM/yyyy HH:mm:ss}";
        if (!string.IsNullOrEmpty(mapControleDeFrota.NomeVeiculo) && (mapControleDeFrota.KmFinal != null && mapControleDeFrota.KmFinal > 0))
        {
            await _veiculoRepository.UpdateKmAtualAsync(mapControleDeFrota.NomeVeiculo, mapControleDeFrota.KmFinal);
        }
        var id = await _controleDeFrotaRepository.AddAsync(mapControleDeFrota);
        return id;
    }

    public async Task<int?> UpdateAsync(ControleDeFrotaViewModel obj)
    {
        var mapControleDeFrota = _mapper.Map<Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>(obj);
        if (!string.IsNullOrEmpty(mapControleDeFrota.Imagem))
        {
            byte[] dataBytes = Convert.FromBase64String(mapControleDeFrota.Imagem);

            string fileName = $"DataRelatorio - {Guid.NewGuid()}.pdf";
            using (var stream = new MemoryStream(dataBytes))
            {
                await _blobStorageRepository.SavePdfAsync(stream, fileName);
            }

            mapControleDeFrota.Imagem = fileName;
        }
        mapControleDeFrota.NomeRelatorio = $"Frota - {mapControleDeFrota.NomeExecutor} - {mapControleDeFrota.NomePiloto} - {mapControleDeFrota.DataCriacao:dd/MM/yyyy HH:mm:ss}";
        if (!string.IsNullOrEmpty(mapControleDeFrota.NomeVeiculo) && (mapControleDeFrota.KmFinal != null && mapControleDeFrota.KmFinal > 0))
        {
            await _veiculoRepository.UpdateKmAtualAsync(mapControleDeFrota.NomeVeiculo, mapControleDeFrota.KmFinal);
        }
        return await _controleDeFrotaRepository.UpdateAsync(mapControleDeFrota);
    }

    public async Task DeleteAsync(int id)
    {
        await _controleDeFrotaRepository.DeleteAsync(id);
    }
}
