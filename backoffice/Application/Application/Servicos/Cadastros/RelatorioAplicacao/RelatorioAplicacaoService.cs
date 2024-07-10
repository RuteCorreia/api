using AutoMapper;
using Domain.Interfaces.Cadastros.RelatorioAplicacao;
using Domain.Entidades.Cadastros.RelatorioAplicacao;
using Application.DTOs.Cadastros.RelatorioAplicacao.ViewModel;
using Helpers;
using Domain.Entidades.Cadastros.Empresa;

namespace Application.Application.Servicos.Cadastros.RelatorioAplicacao
{
    public class RelatorioAplicacaoService : IRelatorioAplicacaoService
    {
        private readonly IRelatorioAplicacaoRepository _relatorioAplicacaoRepository;
        private readonly IMapper _mapper;

        public RelatorioAplicacaoService(IMapper mapper, IRelatorioAplicacaoRepository relatorioAplicacaoRepository)
        {
            _relatorioAplicacaoRepository = relatorioAplicacaoRepository;
            _mapper = mapper;
        }

        public async Task DeleteAsync(int id)
        {
            await _relatorioAplicacaoRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<RelatorioAplicacaoViewModel>> GetAllAsync()
        {
            var list = await _relatorioAplicacaoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RelatorioAplicacaoViewModel>>(list);
        }

        public async Task<RelatorioAplicacaoViewModel> GetByIdAsync(int id)
        {
            var obj = await _relatorioAplicacaoRepository.GetByIdAsync(id);
            return _mapper.Map<RelatorioAplicacaoViewModel>(obj);
        }

        public async Task UpdateAsync(RelatorioAplicacaoViewModel obj)
        {
            var mapProduto = _mapper.Map<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>(obj);
            await _relatorioAplicacaoRepository.UpdateAsync(mapProduto);
        }

        public async Task<RelatorioAplicacaoViewModel> AddAsync(RelatorioAplicacaoViewModel obj, string? idEmpresa)
        {
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var mapRelatorio = _mapper.Map<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>(obj);
            mapRelatorio.IdEmpresa = idEmpresaInt == 0 ? null : idEmpresaInt;
            mapRelatorio.NomeRelatorio = $"Aplicação - {mapRelatorio.Executor} - {mapRelatorio.DataAlteracao}";

            if (obj.Id > 0)
            {
                await _relatorioAplicacaoRepository.UpdateAsync(mapRelatorio);
                var mapRelatorioUpdateReturn = _mapper.Map<RelatorioAplicacaoViewModel>(mapRelatorio);
                return mapRelatorioUpdateReturn;
            }
            else
            {
                var Relatorio = await _relatorioAplicacaoRepository.AddAsync(mapRelatorio);
                var mapRelatorioReturn = _mapper.Map<RelatorioAplicacaoViewModel>(Relatorio);
                return mapRelatorioReturn;
            }
        }

        public async Task<IEnumerable<RelatorioAplicacaoViewModel>> GetAllByIdEmpresaAsync(string? idEmpresa)
        {
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var list = await _relatorioAplicacaoRepository.GetAllByIdEmpresaAsync(idEmpresaInt);
            return _mapper.Map<IEnumerable<RelatorioAplicacaoViewModel>>(list);
        }

        public async Task<IEnumerable<RelatorioAplicacaoViewModel>> GetListByStatusAsync(string? idEmpresa)
        {
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var statusEnvio = 0;
            var list = await _relatorioAplicacaoRepository.GetListByStatusAsync(idEmpresaInt, statusEnvio);
            return _mapper.Map<IEnumerable<RelatorioAplicacaoViewModel>>(list);
        }

        public async Task<IEnumerable<RelatorioAplicacaoViewModel>> GetByDateAndIdEmpresaAsync(DateTime Date, string? idEmpresa)
        {
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var list = await _relatorioAplicacaoRepository.GetAllByIdEmpresaAsync(idEmpresaInt);
            return _mapper.Map<IEnumerable<RelatorioAplicacaoViewModel>>(list);
        }

        public async Task<IEnumerable<RelatorioAplicacaoViewModel>> GetByDataCriacaoAsync(DateTime date, string? idEmpresa)
        {
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var list = await _relatorioAplicacaoRepository.GetByDataCriacaoAsync(date, idEmpresaInt);
            return _mapper.Map<IEnumerable<RelatorioAplicacaoViewModel>>(list);
        }

        public async Task<IEnumerable<RelatorioAplicacaoViewModel>> GetByDataAlteracaoAsync(DateTime date, string? idEmpresa)
        {
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var list = await _relatorioAplicacaoRepository.GetByDataAlteracaoAsync(date, idEmpresaInt);
            return _mapper.Map<IEnumerable<RelatorioAplicacaoViewModel>>(list);
        }

        public async Task<IEnumerable<RelatorioAplicacaoViewModel>> GetNovosAsync(DateTime? offsetDate, string? idEmpresa)
        {
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var list = await _relatorioAplicacaoRepository.GetNovosAsync(offsetDate, idEmpresaInt);
            return _mapper.Map<IEnumerable<RelatorioAplicacaoViewModel>>(list);
        }

    }
}
