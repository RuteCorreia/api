using Application.DTOs.Cadastros.RelatorioIncendio.Interface;
using Application.DTOs.Cadastros.RelatorioIncendio.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.RelatorioIncendio;

namespace Application.Application.Servicos.Cadastros.RelatorioIncendio
{
    public class RelatorioIncendioService : IRelatorioIncendioService
    {
        private readonly IRelatorioIncendioRepository _relatorioIncendioRepository;
        private readonly IMapper _mapper;

        public RelatorioIncendioService(IMapper mapper, IRelatorioIncendioRepository relatorioIncendioRepository)
        {
            _relatorioIncendioRepository = relatorioIncendioRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(RelatorioIncendioViewModel obj)
        {
            var mapProduto = _mapper.Map<Domain.Entidades.Cadastros.RelatorioIncendio.RelatorioIncendio>(obj);
            await _relatorioIncendioRepository.AddAsync(mapProduto);
        }

        public async Task DeleteAsync(int id)
        {
            await _relatorioIncendioRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<RelatorioIncendioViewModel>> GetAllAsync()
        {
            var list = await _relatorioIncendioRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RelatorioIncendioViewModel>>(list);
        }

        public async Task<RelatorioIncendioViewModel> GetByIdAsync(int id)
        {
            var obj = await _relatorioIncendioRepository.GetByIdAsync(id);
            return _mapper.Map<RelatorioIncendioViewModel>(obj);
        }

        public async Task UpdateAsync(RelatorioIncendioViewModel obj)
        {
            var mapProduto = _mapper.Map<Domain.Entidades.Cadastros.RelatorioIncendio.RelatorioIncendio>(obj);
            await _relatorioIncendioRepository.UpdateAsync(mapProduto);
        }
    }
}
