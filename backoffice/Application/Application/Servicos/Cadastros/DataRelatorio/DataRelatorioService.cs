using Application.DTOs.Cadastros.AplicacaoAreaTratada.ViewModel;
using Application.DTOs.Cadastros.DadosResponsavel.ViewModel;
using Application.DTOs.Cadastros.DataRelatorio.Interface;
using Application.DTOs.Cadastros.DataRelatorio.ViewModel;
using Application.DTOs.Cadastros.IdentificacaoAreaTratada.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.DadosResponsavel;
using Domain.Interfaces.Cadastros.DataRelatorio;
using Helpers;

namespace Application.Application.Servicos.Cadastros.DataRelatorio
{
    public class DataRelatorioService : IDataRelatorioService
    {
        private readonly IDataRelatorioRepository _dataRelatorioRepository;
        private readonly IMapper _mapper;

        public DataRelatorioService(
            IMapper mapper,
            IDataRelatorioRepository dataRelatorioRepository
        )
        {
            _dataRelatorioRepository = dataRelatorioRepository;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(DataRelatorioViewModel obj, string? idEmpresa)
        {
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var mapDataRelatorio = _mapper.Map<Domain.Entidades.Cadastros.DataRelatorio.DataRelatorio>(obj);
            mapDataRelatorio.IdEmpresa = idEmpresaInt == 0 ? null : idEmpresaInt;
            return await _dataRelatorioRepository.AddAsync(mapDataRelatorio);
        }

        public async Task DeleteAsync(int id, string? idEmpresa)
        {
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            await _dataRelatorioRepository.DeleteAsync(id, idEmpresaInt);
        }

        public async Task<IEnumerable<DataRelatorioViewModel>> GetAllAsync(string? idEmpresa)
        {
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var list = await _dataRelatorioRepository.GetAllAsync(idEmpresaInt);
            return _mapper.Map<IEnumerable<DataRelatorioViewModel>>(list);
        }

        public async Task<DataRelatorioViewModel?> GetByIdAsync(int? id, string? idEmpresa)
        {
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var obj = await _dataRelatorioRepository.GetByIdAsync(id, idEmpresaInt);
            return _mapper.Map<DataRelatorioViewModel>(obj);
        }

        public async Task<int> UpdateAsync(DataRelatorioViewModel obj)
        {
            var mapDataRelatorio = _mapper.Map<Domain.Entidades.Cadastros.DataRelatorio.DataRelatorio>(obj);
            return await _dataRelatorioRepository.UpdateAsync(mapDataRelatorio);
        }
    }
}
