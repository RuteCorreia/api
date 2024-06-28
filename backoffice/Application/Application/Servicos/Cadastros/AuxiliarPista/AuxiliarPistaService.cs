using Application.DTOs.Cadastros.AuxiliarPista.Interface;
using Application.DTOs.Cadastros.AuxiliarPista.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.AuxiliarPista;

namespace Application.Application.Servicos.Cadastros.AuxiliarPista
{
    public class AuxiliarPistaService : IAuxiliarPistaService
    {
        private readonly IAuxiliarPistaRepository _auxiliarPistaRepository;
        private readonly IMapper _mapper;
        public AuxiliarPistaService(IAuxiliarPistaRepository auxiliarPistaRepository, IMapper mapper)
        {
            _auxiliarPistaRepository = auxiliarPistaRepository;
            _mapper = mapper;
        }
        public async Task<int> AddAsync(AuxiliarPistaViewModel obj)
        {
            var mapIdentificacaoAreaTratada = _mapper.Map<Domain.Entidades.Cadastros.AuxiliarPista.AuxiliarPista>(obj);
            var identificacaoAreaTratada = await _auxiliarPistaRepository.AddAsync(mapIdentificacaoAreaTratada);
            return identificacaoAreaTratada;
        }
    }
}
