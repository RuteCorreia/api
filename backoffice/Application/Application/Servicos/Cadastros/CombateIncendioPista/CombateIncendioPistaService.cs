using Application.DTOs.Cadastros.CombateIncendio.ViewModel;
using Application.DTOs.Cadastros.CombateIncendioPista.Interface;
using Application.DTOs.Cadastros.CombateIncendioPista.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.CombateIncendio;
using Domain.Interfaces.Cadastros.CombateIncendioPista;
using Helpers;
using Infra.Repositorio.Cadastros.CombateIncendio;

namespace Application.Application.Servicos.Cadastros.CombateIncendioPista
{
    public class CombateIncendioPistaService : ICombateIncendioPistaService
    {
        private readonly ICombateIncendioPistaRepository _combateIncendioPistaRepository;
        private readonly IMapper _mapper;
        public CombateIncendioPistaService(ICombateIncendioPistaRepository combateIncendioPistaRepository, IMapper mapper)
        {
            _combateIncendioPistaRepository = combateIncendioPistaRepository;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(CombateIncendioPistaViewModel obj, string? idEmpresa)
        {
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var mapCombateIncendioPista = _mapper.Map<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendioPista>(obj);
            mapCombateIncendioPista.IdEmpresa = idEmpresaInt == 0 ? null : idEmpresaInt;
            var combateIncendio = await _combateIncendioPistaRepository.AddAsync(mapCombateIncendioPista);
            return combateIncendio;
        }

        public async Task DeleteAsync(int id)
        {
            await _combateIncendioPistaRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<CombateIncendioPistaViewModel>> GetAllAsync(string? idEmpresa)
        {
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var list = await _combateIncendioPistaRepository.GetAllAsync(idEmpresaInt);
            return _mapper.Map<IEnumerable<CombateIncendioPistaViewModel>>(list);
        }

        public async Task<IEnumerable<CombateIncendioPistaViewModel>> GetByCombateIncendioIdAsync(int combateIncendioId)
        {
            var list = await _combateIncendioPistaRepository.GetByCombateIncendioIdAsync(combateIncendioId);
            return _mapper.Map<IEnumerable<CombateIncendioPistaViewModel>>(list);
        }

        public async Task<CombateIncendioPistaViewModel> GetByIdAsync(int id)
        {
            var obj = await _combateIncendioPistaRepository.GetByIdAsync(id);
            return _mapper.Map<CombateIncendioPistaViewModel>(obj);
        }

        public async Task<int> UpdateAsync(CombateIncendioPistaViewModel obj, string? idEmpresa)
        {
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var mapCombateIncendioPista = _mapper.Map<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendioPista>(obj);
            mapCombateIncendioPista.IdEmpresa = idEmpresaInt == 0 ? null : idEmpresaInt;
            return await _combateIncendioPistaRepository.UpdateAsync(mapCombateIncendioPista);
        }
    }
}
