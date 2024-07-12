using Application.DTOs.Cadastros.Tipo_Produto.ViewModel;
using Application.DTOs.Cadastros.TipoDeServico.Interface;
using Application.DTOs.Cadastros.TipoDeServico.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.TipoDeServico;
using Domain.Interfaces.Cadastros.TipoProduto;

namespace Application.Application.Servicos.Cadastros.TipoDeServico
{
    public class TipoDeServicoService : ITipoDeServicoService
    {
        private readonly ITipoDeServicoRepository _tipoDeServicoRepository;
        private readonly IMapper _mapper;

        public TipoDeServicoService(IMapper mapper, ITipoDeServicoRepository tipoDeServicoRepository)
        {
            _tipoDeServicoRepository = tipoDeServicoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TipoDeServicoViewModel>> GetAllAsync()
        {
            var list = await _tipoDeServicoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TipoDeServicoViewModel>>(list);
        }

        public async Task<TipoDeServicoViewModel> GetByIdAsync(int id)
        {
            var obj = await _tipoDeServicoRepository.GetByIdAsync(id);
            return _mapper.Map<TipoDeServicoViewModel>(obj);
        }

        public async Task AddAsync(TipoDeServicoViewModel obj)
        {
            var mapTipoProduto = _mapper.Map<Domain.Entidades.Cadastros.TipoDeServico.TipoDeServico>(obj);
            await _tipoDeServicoRepository.AddAsync(mapTipoProduto);
        }

        public async Task UpdateAsync(TipoDeServicoViewModel obj)
        {
            var mapTipoProduto = _mapper.Map<Domain.Entidades.Cadastros.TipoDeServico.TipoDeServico>(obj);
            await _tipoDeServicoRepository.UpdateAsync(mapTipoProduto);
        }

        public async Task DeleteAsync(int id)
        {
            await _tipoDeServicoRepository.DeleteAsync(id);
        }
    }
}
