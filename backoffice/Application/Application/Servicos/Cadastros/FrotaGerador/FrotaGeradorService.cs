using Application.DTOs.Cadastros.FrotaGerador.Interface;
using Application.DTOs.Cadastros.FrotaGerador.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.FrotaGerador;
using Helpers;

namespace Application.Application.Servicos.Cadastros.FrotaGerador
{
    public class FrotaGeradorService : IFrotaGeradorService
    {
        private readonly IFrotaGeradorRepository _frotaGeradorRepository;
        private readonly IMapper _mapper;
        public FrotaGeradorService(
            IFrotaGeradorRepository frotaGeradorRepository,
            IMapper mapper)
        {
            _frotaGeradorRepository = frotaGeradorRepository;
            _mapper = mapper;
        }
        public async Task<int> AddAsync(FrotaGeradorViewModel obj, string? idEmpresa)
        {
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var mapFrotaGerador = _mapper.Map<Domain.Entidades.Cadastros.FrotaGerador.FrotaGerador>(obj);
            mapFrotaGerador.IdEmpresa = idEmpresaInt;
            return await _frotaGeradorRepository.AddAsync(mapFrotaGerador);
        }

        public async Task DeleteAsync(int id)
        {
            await _frotaGeradorRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<FrotaGeradorViewModel>> GetAllAsync(string? idEmpresa)
        {
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var list = await _frotaGeradorRepository.GetAllAsync(idEmpresaInt);
            return _mapper.Map<IEnumerable<FrotaGeradorViewModel>>(list);
        }

        public async Task<IEnumerable<FrotaGeradorViewModel>> GetByIdAsync(int? id)
        {
            var obj = await _frotaGeradorRepository.GetByIdAsync(id);
            return _mapper.Map<IEnumerable<FrotaGeradorViewModel>>(obj);
        }

        public async Task<int> UpdateAsync(FrotaGeradorViewModel obj)
        {
            var mapFrotaGerador = _mapper.Map<Domain.Entidades.Cadastros.FrotaGerador.FrotaGerador>(obj);
            return await _frotaGeradorRepository.UpdateAsync(mapFrotaGerador);
        }
    }
}
