using Application.DTOs.Cadastros.TipoDeServico.ViewModel;
using Application.DTOs.Cadastros.TipoDeUnidade.Interface;
using Application.DTOs.Cadastros.TipoDeUnidade.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.TipoDeServico;
using Domain.Interfaces.Cadastros.TipoDeUnidade;
using Infra.Repositorio.Cadastros.TipoDeServico;

namespace Application.Application.Servicos.Cadastros.TipoDeUnidade
{
    public class TipoDeUnidadeService : ITipoDeUnidadeService
    {
        private readonly ITipoDeUnidadeRepository _tipoDeUnidadeRepository;
        private readonly IMapper _mapper;

        public TipoDeUnidadeService(IMapper mapper, ITipoDeUnidadeRepository tipoDeUnidadeRepository)
        {
            _tipoDeUnidadeRepository = tipoDeUnidadeRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TipoDeUnidadeViewModel>> GetAllAsync()
        {
            var list = await _tipoDeUnidadeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TipoDeUnidadeViewModel>>(list);
        }
    }
}
