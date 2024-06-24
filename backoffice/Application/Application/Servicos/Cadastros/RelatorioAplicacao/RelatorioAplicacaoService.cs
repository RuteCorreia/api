using Application.DTOs.Cadastros.RelatorioAplicacao.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.RelatorioAplicacao;

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

        public async Task AddAsync(RelatorioAplicacaoViewModel obj)
        {
            var mapProduto = _mapper.Map<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>(obj);
            await _relatorioAplicacaoRepository.AddAsync(mapProduto);
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

        Task<int> IRelatorioAplicacaoService.AddAsync(RelatorioAplicacaoViewModel obj)
        {
            throw new NotImplementedException();
        }
    }
}
