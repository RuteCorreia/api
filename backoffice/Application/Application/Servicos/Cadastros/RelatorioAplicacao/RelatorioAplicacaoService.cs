using AutoMapper;
using Domain.Interfaces.Cadastros.RelatorioAplicacao;
using Domain.Entidades.Cadastros.RelatorioAplicacao;
using Application.DTOs.Cadastros.RelatorioAplicacao.ViewModel;

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
            var mapProduto = _mapper.Map<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao> (obj);
            await _relatorioAplicacaoRepository.UpdateAsync(mapProduto);
        }

        public async Task<RelatorioAplicacaoViewModel> AddAsync(RelatorioAplicacaoViewModel obj)
        {
            var mapRelatorio = _mapper.Map<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>(obj);
            var Relatorio = await _relatorioAplicacaoRepository.AddAsync(mapRelatorio); 
            var mapRelatorioReturn = _mapper.Map< RelatorioAplicacaoViewModel>(Relatorio);
            return mapRelatorioReturn;
        }
    }
}
