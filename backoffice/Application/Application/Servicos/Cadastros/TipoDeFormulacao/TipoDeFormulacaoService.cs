using Application.DTOs.Cadastros.TipoDeFormulacao.Interfaces;
using Application.DTOs.Cadastros.TipoDeFormulacao.ViewModel;
using Application.DTOs.Cadastros.TipoDeServico.Interface;
using Application.DTOs.Cadastros.TipoDeServico.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.TipoDeFormulacao;
using Domain.Interfaces.Cadastros.TipoDeServico;

namespace Application.Application.Servicos.Cadastros.TipoDeFormulacao
{
    public class TipoDeFormulacaoService : ITipoDeFormulacaoService
    {
        private readonly ITipoDeFormulacaoRepository _tipoDeFormulacaoRepository;
        private readonly IMapper _mapper;

        public TipoDeFormulacaoService(IMapper mapper, ITipoDeFormulacaoRepository tipoDeFormulacaoRepository)
        {
            _tipoDeFormulacaoRepository = tipoDeFormulacaoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TipoDeFormulacaoViewModel>> GetAllAsync()
        {
            var list = await _tipoDeFormulacaoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TipoDeFormulacaoViewModel>>(list);
        }

        public async Task<TipoDeFormulacaoViewModel> GetByIdAsync(int id)
        {
            var obj = await _tipoDeFormulacaoRepository.GetByIdAsync(id);
            return _mapper.Map<TipoDeFormulacaoViewModel>(obj);
        }

        public async Task AddAsync(TipoDeFormulacaoViewModel obj)
        {
            var mapTipoDeFormulacao = _mapper.Map<Domain.Entidades.Cadastros.TipoDeFormulacao.TipoDeFormulacao>(obj);
            await _tipoDeFormulacaoRepository.AddAsync(mapTipoDeFormulacao);
        }

        public async Task UpdateAsync(TipoDeFormulacaoViewModel obj)
        {
            var mapTipoDeFormulacao = _mapper.Map<Domain.Entidades.Cadastros.TipoDeFormulacao.TipoDeFormulacao>(obj);
            await _tipoDeFormulacaoRepository.UpdateAsync(mapTipoDeFormulacao);
        }

        public async Task DeleteAsync(int id)
        {
            await _tipoDeFormulacaoRepository.DeleteAsync(id);
        }
    }
}
