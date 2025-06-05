using Application.DTOs.Cadastros.AplicacaoRelatorio.Mappings;
using Application.DTOs.Cadastros.DataFormat.ViewModel;
using Application.DTOs.Cadastros.ReceituarioAgronomico.Interface;
using Application.DTOs.Cadastros.ReceituarioAgronomico.ViewModel;
using AutoMapper;
using Azure;
using Domain.Entidades.Cadastros.ReceituarioAgronomico;
using Domain.Interfaces.BlobStorage;
using Domain.Interfaces.Cadastros.ReceituarioAgronomico;
using Newtonsoft.Json;

namespace Application.Application.Servicos.Cadastros.CaracteristicasReceituarioAgronomico;

public class ReceituarioAgronomicoService : IReceituarioAgronomicoService
{
    private readonly IReceituarioAgronomicoRepository _receituarioAgronomicoRepository;
    private readonly IBlobStorageRepository _blobStorageRepository;
    private readonly IMapper _mapper;

    public ReceituarioAgronomicoService(
        IMapper mapper,
        IBlobStorageRepository blobStorageRepository,
        IReceituarioAgronomicoRepository receituarioAgronomicoRepository
    )
    {
        _mapper = mapper;
        _blobStorageRepository = blobStorageRepository;
        _receituarioAgronomicoRepository = receituarioAgronomicoRepository;
    }

    public async Task<int> AddAsync(ReceituarioAgronomicoViewModel obj)
    {
        var receituario = _mapper.Map<ReceituarioAgronomico>(obj);
        receituario.NomeArquivo = await UploadBlobFile(obj.NomeArquivo);

        return await _receituarioAgronomicoRepository.AddAsync(receituario);
    }

    public async Task UpdateAsync(ReceituarioAgronomicoViewModel obj)
    {
        var receituario = _mapper.Map<ReceituarioAgronomico>(obj);
        receituario.NomeArquivo = await UploadBlobFile(obj.NomeArquivo);

        await _receituarioAgronomicoRepository.UpdateAsync(receituario);
    }

    public async Task DeleteAsync(int id)
    {
        await _receituarioAgronomicoRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<ReceituarioAgronomicoViewModel>> GetAllByIdRelatorioAplicacaoAsync(int relatorioAplicacaoId)
    {
        var receituarios = await _receituarioAgronomicoRepository.GetAllByIdRelatorioAplicacaoAsync(relatorioAplicacaoId);

        List<ReceituarioAgronomicoViewModel> responses = new List<ReceituarioAgronomicoViewModel>();

        foreach (var r in receituarios)
            responses.Add(await MapResponse(r));

        return responses;
    }

    public async Task<ReceituarioAgronomicoViewModel> GetByIdAsync(int id)
    {
        var receituario = await _receituarioAgronomicoRepository.GetByIdAsync(id);

        return await MapResponse(receituario);
    }

    private async Task<string> GetReceituarioFileBase64(string? nomeArquivo)
    {
        if (!string.IsNullOrEmpty(nomeArquivo) && nomeArquivo != "{\"Format\":\"raw\",\"Data\":null}")
        {
            var receiturarioAgronomico = await _blobStorageRepository.GetPdfAsync(nomeArquivo);
            string receiturarioAgronomicoBase64 = "";
            using (var memoryStream = new MemoryStream())
            {
                await receiturarioAgronomico.CopyToAsync(memoryStream);
                var byteArray = memoryStream.ToArray();
                receiturarioAgronomicoBase64 = Convert.ToBase64String(byteArray);
            }

            return receiturarioAgronomicoBase64;
        }

        return "";
    }

    private async Task<ReceituarioAgronomicoViewModel> MapResponse(ReceituarioAgronomico receituario)
    {
        var fileExtension = Path.GetExtension(receituario.NomeArquivo)?.ToLower().TrimStart('.');

        var response = _mapper.Map<ReceituarioAgronomicoViewModel>(receituario);
        response.NomeArquivo.Data = await GetReceituarioFileBase64(receituario.NomeArquivo);
        response.NomeArquivo.Format = fileExtension;
        response.NomeArquivoStr = JsonConvert.SerializeObject(response.NomeArquivo);

        return response;
    }

    private async Task<string> UploadBlobFile(DataFormatViewModel dataFormat)
    {
        string fileName = "";
        if (!string.IsNullOrEmpty(dataFormat.Data))
        {
            byte[] receituarioAgronomicoBytes = Convert.FromBase64String(dataFormat.Data);

            fileName = $"ReceituarioAgronomico - {Guid.NewGuid()}.{dataFormat.Format}";
            using (var stream = new MemoryStream(receituarioAgronomicoBytes))
            {
                await _blobStorageRepository.SavePdfAsync(stream, fileName);
            }
        }

        return fileName;
    }
}
