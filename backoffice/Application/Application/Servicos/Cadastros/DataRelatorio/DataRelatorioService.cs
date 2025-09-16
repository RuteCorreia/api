using Application.DTOs.Cadastros.DataRelatorio.Interface;
using Application.DTOs.Cadastros.DataRelatorio.ViewModel;
using AutoMapper;
using Domain.Interfaces.BlobStorage;
using Domain.Interfaces.Cadastros.DataRelatorio;
using Helpers;

namespace Application.Application.Servicos.Cadastros.DataRelatorio
{
    public class DataRelatorioService : IDataRelatorioService
    {
        private readonly IDataRelatorioRepository _dataRelatorioRepository;
        private readonly IBlobStorageRepository _blobStorageRepository;
        private readonly IMapper _mapper;

        public DataRelatorioService(
            IMapper mapper,
            IDataRelatorioRepository dataRelatorioRepository,
            IBlobStorageRepository blobStorageRepository
        )
        {
            _dataRelatorioRepository = dataRelatorioRepository;
            _blobStorageRepository = blobStorageRepository;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(DataRelatorioViewModel obj, string? idEmpresa)
        {
            var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
            var mapDataRelatorio = _mapper.Map<Domain.Entidades.Cadastros.DataRelatorio.DataRelatorio>(obj);
            mapDataRelatorio.IdEmpresa = idEmpresaInt == 0 ? null : idEmpresaInt;
            if (!string.IsNullOrEmpty(mapDataRelatorio.Data))
            {
                byte[] dataBytes = Convert.FromBase64String(mapDataRelatorio.Data);

                string fileName = $"DataRelatorio - {Guid.NewGuid()}.pdf";
                using (var stream = new MemoryStream(dataBytes))
                {
                    await _blobStorageRepository.SavePdfAsync(stream, fileName);
                }

                mapDataRelatorio.Data = fileName;
            }
            return await _dataRelatorioRepository.AddAsync(mapDataRelatorio);
        }

        public async Task DeleteAsync(int id, string? idEmpresa)
        {
            var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
            await _dataRelatorioRepository.DeleteAsync(id, idEmpresaInt);
        }

        public async Task<IEnumerable<DataRelatorioViewModel>> GetAllAsync(string? idEmpresa)
        {
            var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
            var list = await _dataRelatorioRepository.GetAllAsync(idEmpresaInt);
            return _mapper.Map<IEnumerable<DataRelatorioViewModel>>(list);
        }

        public async Task<DataRelatorioViewModel?> GetByIdAsync(int? id, string? idEmpresa)
        {
            var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
            var obj = await _dataRelatorioRepository.GetByIdAsync(id, idEmpresaInt);
            var viewModel = _mapper.Map<DataRelatorioViewModel>(obj);
            if (!string.IsNullOrEmpty(viewModel.Data))
            {
                var data = await _blobStorageRepository.GetPdfAsync(viewModel.Data);
                using (var memoryStream = new MemoryStream())
                {
                    await data.CopyToAsync(memoryStream);
                    var fileBytes = memoryStream.ToArray();

                    string base64String = Convert.ToBase64String(fileBytes);

                    viewModel.Data = base64String;
                }
            }
            return viewModel;
        }

        public async Task<string> GerarLinksPdf(string base64Pdf)
        {
            byte[] pdfBytes = Convert.FromBase64String(base64Pdf);

            // Converte os bytes do PDF para uma string data URI
            string dataUri = $"data:application/pdf;base64,{Convert.ToBase64String(pdfBytes)}";

            return dataUri;
        }

        public async Task<int> UpdateAsync(DataRelatorioViewModel obj)
        {
            var mapDataRelatorio = _mapper.Map<Domain.Entidades.Cadastros.DataRelatorio.DataRelatorio>(obj);
            if (!string.IsNullOrEmpty(mapDataRelatorio.Data))
            {
                byte[] dataBytes = Convert.FromBase64String(mapDataRelatorio.Data);

                string fileName = $"DataRelatorio - {Guid.NewGuid()}.pdf";
                using (var stream = new MemoryStream(dataBytes))
                {
                    await _blobStorageRepository.SavePdfAsync(stream, fileName);
                }

                mapDataRelatorio.Data = fileName;
            }
            return await _dataRelatorioRepository.UpdateAsync(mapDataRelatorio);
        }
    }
}
