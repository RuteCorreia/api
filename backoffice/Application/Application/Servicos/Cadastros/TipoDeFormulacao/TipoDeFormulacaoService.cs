using Application.DTOs.Cadastros.AlvoBiologico.ViewModel;
using Application.DTOs.Cadastros.TipoDeFormulacao.Interfaces;
using Application.DTOs.Cadastros.TipoDeFormulacao.ViewModel;
using Application.DTOs.Cadastros.TipoDeServico.Interface;
using Application.DTOs.Cadastros.TipoDeServico.ViewModel;
using AutoMapper;
using Domain.Entidades.Cadastros.Empresa;
using Domain.Interfaces.Cadastros.TipoDeFormulacao;
using Domain.Interfaces.Cadastros.TipoDeServico;
using Helpers;

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

        public async Task<IEnumerable<TipoDeFormulacaoViewModel>> GetAllAsync(string? idEmpresa)
        {
            var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
            var list = await _tipoDeFormulacaoRepository.GetAllAsync(idEmpresaInt);
            return _mapper.Map<IEnumerable<TipoDeFormulacaoViewModel>>(list);
        }

        public async Task<TipoDeFormulacaoViewModel> GetByNameAsync(string name, string? idEmpresa)
        {
            var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
            var obj = await _tipoDeFormulacaoRepository.GetByNameAsync(name, idEmpresaInt);
            return _mapper.Map<TipoDeFormulacaoViewModel>(obj);
        }

        public async Task<TipoDeFormulacaoViewModel> GetByIdAsync(int id)
        {
            var obj = await _tipoDeFormulacaoRepository.GetByIdAsync(id);
            return _mapper.Map<TipoDeFormulacaoViewModel>(obj);
        }

        public async Task AddAsync(TipoDeFormulacaoViewModel obj, string? idEmpresa)
        {
            var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
            var mapTipoDeFormulacao = _mapper.Map<Domain.Entidades.Cadastros.TipoDeFormulacao.TipoDeFormulacao>(obj);
            mapTipoDeFormulacao.IdEmpresa = idEmpresaInt;
            mapTipoDeFormulacao.CampoExcluido = 0;
            await _tipoDeFormulacaoRepository.AddAsync(mapTipoDeFormulacao);
        }

        public async Task UpdateAsync(TipoDeFormulacaoViewModel obj, string? idEmpresa)
        {
            var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
            var mapTipoDeFormulacao = _mapper.Map<Domain.Entidades.Cadastros.TipoDeFormulacao.TipoDeFormulacao>(obj);
            await _tipoDeFormulacaoRepository.UpdateAsync(mapTipoDeFormulacao, idEmpresaInt);
        }

        public async Task DeleteAsync(int id, string? idEmpresa)
        {
            var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
            await _tipoDeFormulacaoRepository.DeleteAsync(id, idEmpresaInt);
        }
    }
}
