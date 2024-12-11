using Application.DTOs.Cadastros.AplicacaoAreaTratada.ViewModel;
using Application.DTOs.Cadastros.DataFormat.ViewModel;
using Application.DTOs.Cadastros.Frota.ViewModel;
using Application.DTOs.Cadastros.IdentificacaoAreaTratada.Interface;
using Application.DTOs.Cadastros.IdentificacaoAreaTratada.ViewModel;
using AutoMapper;
using Domain.Interfaces.BlobStorage;
using Domain.Interfaces.Cadastros.Frota;
using Domain.Interfaces.Cadastros.IdentificacaoAreaTratada;
using Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Application.Servicos.Cadastros.IdentificacaoAreaTratada
{
    public class IdentificacaoAreaTratadaService : IIdentificacaoAreaTratadaService
    {
        private readonly IIdentificacaoAreaTratadaRepository _identificacaoAreaTratadaRepository;
        private readonly IBlobStorageRepository _blobStorageRepository;
        private readonly IMapper _mapper;

        public IdentificacaoAreaTratadaService(IMapper mapper,
            IBlobStorageRepository blobStorageRepository,
            IIdentificacaoAreaTratadaRepository identificacaoAreaTratadaRepository)
        {
            _identificacaoAreaTratadaRepository = identificacaoAreaTratadaRepository;
            _blobStorageRepository = blobStorageRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<IdentificacaoAreaTratadaViewModel>> GetAllAsync()
        {
            var list = await _identificacaoAreaTratadaRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<IdentificacaoAreaTratadaViewModel>>(list);
        }

        public async Task<AreaTratadaViewModel> GetByIdAsync(int id)
        {
            var obj = await _identificacaoAreaTratadaRepository.GetByIdAsync(id);
            var mapIdentificacaoAreaTratada = _mapper.Map<AreaTratadaViewModel>(obj);
            if (!string.IsNullOrEmpty(mapIdentificacaoAreaTratada.CroquiArea))
            {
                var fileExtension = Path.GetExtension(mapIdentificacaoAreaTratada.CroquiArea)?.ToLower().TrimStart('.');
                var croquiArea = await _blobStorageRepository.GetPdfAsync(mapIdentificacaoAreaTratada.CroquiArea);
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

                mapIdentificacaoAreaTratada.CroquiArea = JsonConvert.SerializeObject(croquiAreaDataFormat);
            }
            return mapIdentificacaoAreaTratada;
        }

        public async Task<int> AddAsync(AreaTratadaViewModel obj, string? idEmpresa)
        {
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var mapIdentificacaoAreaTratada = _mapper.Map<Domain.Entidades.Cadastros.IdentificacaoAreaTratada.IdentificacaoAreaTratada>(obj);
            mapIdentificacaoAreaTratada.IdEmpresa = idEmpresaInt == 0 ? null : idEmpresaInt;
            DataFormatViewModel croquiAreaDataFormat;
            if (!string.IsNullOrEmpty(mapIdentificacaoAreaTratada.CroquiArea))
            {
                croquiAreaDataFormat = JsonConvert.DeserializeObject<DataFormatViewModel>(mapIdentificacaoAreaTratada.CroquiArea);
                if (!string.IsNullOrEmpty(croquiAreaDataFormat.Data))
                {
                    byte[] croquiAreaBytes = Convert.FromBase64String(croquiAreaDataFormat.Data);

                    string fileName = $"CroquiArea - {Guid.NewGuid()}.{croquiAreaDataFormat.Format}";
                    using (var stream = new MemoryStream(croquiAreaBytes))
                    {
                        await _blobStorageRepository.SavePdfAsync(stream, fileName);
                    }

                    mapIdentificacaoAreaTratada.CroquiArea = fileName; 
                }
                else
                {
                    mapIdentificacaoAreaTratada.CroquiArea = string.Empty;
                }
            }

            if (obj.Id > 0) 
            {
                await _identificacaoAreaTratadaRepository.UpdateAsync(mapIdentificacaoAreaTratada);
                return mapIdentificacaoAreaTratada.Id;
            }  else {
                return await _identificacaoAreaTratadaRepository.AddAsync(mapIdentificacaoAreaTratada);
            }
        }

        public async Task UpdateAsync(IdentificacaoAreaTratadaViewModel obj)
        {
            var mapIdentificacaoAreaTratada = _mapper.Map<Domain.Entidades.Cadastros.IdentificacaoAreaTratada.IdentificacaoAreaTratada>(obj);
            await _identificacaoAreaTratadaRepository.UpdateAsync(mapIdentificacaoAreaTratada);
        }

        public async Task DeleteAsync(int id)
        {
            await _identificacaoAreaTratadaRepository.DeleteAsync(id);
        }

        public async Task<AreaTratadaViewModel> GetForExportExcelAsync(int id)
        {
            var obj = await _identificacaoAreaTratadaRepository.GetForExportExcelAsync(id);
            return _mapper.Map<AreaTratadaViewModel>(obj);
        }
    }
}
