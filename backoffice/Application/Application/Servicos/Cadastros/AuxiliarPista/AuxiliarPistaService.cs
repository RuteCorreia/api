using Application.DTOs.Cadastros.AuxiliarPista.Interface;
using Application.DTOs.Cadastros.AuxiliarPista.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.AuxiliarPista;
using Helpers;

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
        public async Task<int> AddAsync(AuxiliarPistaViewModel obj, string? idEmpresa)
        {
            if (obj == null)
                return 0;

            var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
            var mapAuxiliarPista = _mapper.Map<Domain.Entidades.Cadastros.AuxiliarPista.AuxiliarPista>(obj);
            mapAuxiliarPista.IdEmpresa = idEmpresaInt == 0 ? null : idEmpresaInt;

            //if (obj.Id > 0)
            //    await _auxiliarPistaRepository.UpdateAsync(mapAuxiliarPista);

            var auxiliarPista = await _auxiliarPistaRepository.AddAsync(mapAuxiliarPista);
            return auxiliarPista;
        }

        public async Task<AuxiliarPistaViewModel?> GetByIdAsync(int id)
        {
            var entity = await _auxiliarPistaRepository.GetByIdAsync(id);
            if (entity == null) return null;
            return _mapper.Map<AuxiliarPistaViewModel>(entity);
        }
    }
}
